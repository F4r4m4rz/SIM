﻿using Microsoft.CSharp;
using SIM.CodeEngine.Dynamic;
using SIM.Core.Attributes;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Assembly
{
    /// <summary>
    /// This class generates .cs files for dynamic objects
    /// </summary>
    internal class CSharpCodeGenerator
    {
        private readonly DynamicObject dynamicObject;
        private readonly string outputFolder;
        private CodeCompileUnit compileUnit;

        private CSharpCodeGenerator()
        {
            compileUnit = new CodeCompileUnit();
        }

        /// <summary>
        /// Class to generate .CS file
        /// </summary>
        /// <param name="dynamicObject"></param>
        public CSharpCodeGenerator(DynamicObject dynamicObject) : this()
        {
            this.dynamicObject = dynamicObject ??
                throw new ArgumentNullException(nameof(dynamicObject));
        }

        /// <summary>
        /// Class to generate .CS file
        /// </summary>
        /// <param name="dynamicObject"></param>
        /// <param name="outputFolder">Folder where the files should be stored</param>
        public CSharpCodeGenerator(DynamicObject dynamicObject, string outputFolder) : this(dynamicObject)
        {
            if (string.IsNullOrWhiteSpace(outputFolder))
            {
                throw new ArgumentException("Output folder is not defined", nameof(outputFolder));
            }

            
            this.outputFolder = outputFolder;
            ValidateOutputFolder();
        }

        private void ValidateOutputFolder()
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
        }

        /// <summary>
        /// Generates the C# code for the dynamycObject using CodeDOM
        /// </summary>
        /// <param name="asFile"></param>
        /// <returns>if asFile is true CodeCompileUnit will be returned, else the file path</returns>
        public dynamic GenerateCode(bool asFile)
        {
            // Build code hierarchy
            var @namespace = GenerateNamespace();
            var @class = GenerateClass(@namespace);
            GenerateClassAttributes(@class);

            if (dynamicObject is DynamicGraphObject)
                GenerateProperties(@class);

            // Build C# code
            if (!asFile) return compileUnit;

            return GenerateFileCode();
        }

        private void GenerateClassAttributes(CodeTypeDeclaration @class)
        {
            for (int i = 0; i < dynamicObject.Attributes.Count; i++)
            {
                var dynamicAtt = dynamicObject.Attributes.ElementAt(i);
                var attribute = GenerateAttribute(dynamicAtt);
                @class.CustomAttributes.Add(attribute);
            }
        }

        private CodeAttributeDeclaration GenerateAttribute(KeyValuePair<Type, object[]> dynamicAtt)
        {
            var attribute = new CodeAttributeDeclaration(new CodeTypeReference(dynamicAtt.Key));
            
            // Collect parameters of constructor
            var parameters = dynamicAtt.Key.GetConstructors()[0].GetParameters();

            // For each parameter generate a CodeAttributeArgument and add to attribute
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType.BaseType == typeof(Enum))
                    attribute.Arguments.Add(new CodeAttributeArgument(
                        new CodePropertyReferenceExpression(
                            new CodeVariableReferenceExpression(parameters[i].ParameterType.Name), dynamicAtt.Value[i].ToString())));

                if (parameters[i].ParameterType == typeof(string))
                    attribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(dynamicAtt.Value[i])));

                if (parameters[i].ParameterType == typeof(Type))
                    attribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(dynamicAtt.Value[i].ToString())));
            }

            return attribute;
        }

        private string GenerateFileCode()
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            string outputPath = GenerateOuputPath();

            using (StreamWriter streamWriter = new StreamWriter(outputPath, false))
            using (IndentedTextWriter tw = new IndentedTextWriter(streamWriter, "    "))
            {
                provider.GenerateCodeFromCompileUnit(compileUnit, tw, new CodeGeneratorOptions());
            }

            return outputPath;
        }

        private string GenerateOuputPath()
        {

            // Build source output path
            var outputDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var outputSubDir = Path.Combine("SIM", "Auto generated codes", dynamicObject.Namespace);
            outputDir = Path.Combine(outputDir, outputSubDir);

            // Check of the path exists
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            var outputFile = dynamicObject.Name;
            var outputPath = Path.Combine(outputDir, outputFile + ".cs");
            return outputPath;
        }

        private void GenerateProperties(CodeTypeDeclaration @class)
        {
            for (int i = 0; i < (dynamicObject as DynamicGraphObject).Properties.Count; i++)
            {
                var dynamicProp = (dynamicObject as DynamicGraphObject).Properties.ElementAt(i);
                //var field = GeenerateField(dynamicProp);
                //@class.Members.Add(field);

                var property = GeneratePrperty(dynamicProp);
                GeneratePropertyAttributes(dynamicProp, property);
                @class.Members.Add(property);
            }
        }

        private void GeneratePropertyAttributes(DynamicProperty dynamicProp, CodeMemberProperty property)
        {
            // Data type attribute
            if (dynamicProp is DynamicRelationProperty)
                GenerateProperyDataTypeAttribute(dynamicProp, property, typeof(PropertyRelationTargetTypeAttribute));
            //else
            //    GenerateProperyDataTypeAttribute(dynamicProp, property, typeof(PropertyNodeTypeAttribute));

            // Reuired attribute
            if (dynamicProp.IsRequired)
                GenerateNoArgumentAttribute(dynamicProp, property, typeof(RequiredAttribute));

            // UserInput attribute
            if (dynamicProp.IsUserInput)
                GenerateNoArgumentAttribute(dynamicProp, property, typeof(UserInputAttribute));

            // First level attribute
            if (dynamicProp.IsFirstLevelProperty)
                GenerateNoArgumentAttribute(dynamicProp, property, typeof(FirstLevelPropertyAttribute));
        }

        private void GenerateNoArgumentAttribute(DynamicProperty dynamicProp, CodeMemberProperty property, Type attributeType)
        {
            var attribute = new CodeAttributeDeclaration(new CodeTypeReference(attributeType));
            property.CustomAttributes.Add(attribute);
        }

        private void GenerateProperyDataTypeAttribute(DynamicProperty dynamicProp, CodeMemberProperty property, Type attributeType)
        {
            var attribute = new CodeAttributeDeclaration(new CodeTypeReference(attributeType));
            //if (dynamicProp is DynamicRelationProperty)
            //    attribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression((dynamicProp as DynamicRelationProperty).TargetNodeType)));
            //else
            attribute.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(dynamicProp.ValueType)));
            property.CustomAttributes.Add(attribute);
        }

        private CodeMemberProperty GeneratePrperty(DynamicProperty dynamicProp)
        {
            CodeMemberProperty property = new CodeMemberProperty();
            property.Name = dynamicProp.Name;
            property.Type = new CodeTypeReference(dynamicProp.DerivedFrom);
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;

            // Generate getter
            CodeMethodReturnStatement getter = GenerateGetter(property);
            property.GetStatements.Add(getter);

            // Generate setter
            CodeAssignStatement setter = GenerateSetter(property);
            property.SetStatements.Add(setter);

            return property;
        }

        private CodeAssignStatement GenerateSetter(CodeMemberProperty property)
        {
            CodeAssignStatement setter =
                new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), $"Properties[\"{property.Name}\"]")
                    , new CodePropertySetValueReferenceExpression());

            return setter;
        }

        private CodeMethodReturnStatement GenerateGetter(CodeMemberProperty property)
        {
            CodeMethodReturnStatement getter =
                new CodeMethodReturnStatement(
                    new CodeCastExpression(property.Type,
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(), $"Properties[\"{property.Name}\"]")));
            
            return getter;
        }

        private CodeMemberField GeenerateField(DynamicProperty dynamicProperty)
        {
            CodeMemberField field = new CodeMemberField();
            field.Name = '_' + dynamicProperty.Name;
            field.Type = new CodeTypeReference(dynamicProperty.DerivedFrom);
            field.Attributes = MemberAttributes.Private;

            return field;
        }

        private CodeTypeDeclaration GenerateClass(CodeNamespace ns)
        {
            CodeTypeDeclaration cs = new CodeTypeDeclaration(dynamicObject.Name);

            // Define base class
            cs.BaseTypes.Add(new CodeTypeReference(dynamicObject.DerivedFrom));
            cs.IsPartial = true;
            cs.TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Sealed;
            
            // Add type to namespace
            ns.Types.Add(cs);

            return cs;
        }

        private CodeNamespace GenerateNamespace()
        {
            CodeNamespace ns = new CodeNamespace(dynamicObject.Namespace);

            // import system and SIM.Core
            CodeNamespaceImport system = new CodeNamespaceImport("System");
            CodeNamespaceImport simCore = new CodeNamespaceImport("SIM.Core.Objects");
            CodeNamespaceImport dataAnnotation = new CodeNamespaceImport("System.ComponentModel.DataAnnotations");
            ns.Imports.Add(system);
            ns.Imports.Add(simCore);
            ns.Imports.Add(dataAnnotation);

            // Add new namespace
            compileUnit.Namespaces.Add(ns);

            return ns;
        }
    }
}
