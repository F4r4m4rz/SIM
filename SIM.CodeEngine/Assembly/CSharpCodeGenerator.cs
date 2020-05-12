using Microsoft.CSharp;
using SIM.CodeEngine.Dynamic;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
            GenerateProperties(@class);

            // Build C# code
            if (!asFile) return compileUnit;

            return GenerateFileCode();
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
            for (int i = 0; i < dynamicObject.Properties.Count; i++)
            {
                var dynamicProp = dynamicObject.Properties.ElementAt(i);
                var field = GeenerateField(dynamicProp);
                @class.Members.Add(field);

                var property = GeneratePrperty(dynamicProp);
                @class.Members.Add(property);
            }
        }

        private CodeMemberProperty GeneratePrperty(DynamicProperty dynamicProp)
        {
            CodeMemberProperty property = new CodeMemberProperty();
            property.Name = dynamicProp.PropertyName;
            property.Type = new CodeTypeReference(dynamicProp.PropertyType);
            property.Attributes = MemberAttributes.Public;

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
                        new CodeThisReferenceExpression(), '_' + property.Name)
                    , new CodePropertySetValueReferenceExpression());

            return setter;
        }

        private CodeMethodReturnStatement GenerateGetter(CodeMemberProperty property)
        {
            CodeMethodReturnStatement getter =
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), '_' + property.Name));
            
            return getter;
        }

        private CodeMemberField GeenerateField(DynamicProperty dynamicProperty)
        {
            CodeMemberField field = new CodeMemberField();
            field.Name = '_' + dynamicProperty.PropertyName;
            field.Type = new CodeTypeReference(dynamicProperty.PropertyType);
            field.Attributes = MemberAttributes.Private;

            return field;
        }

        private CodeTypeDeclaration GenerateClass(CodeNamespace ns)
        {
            CodeTypeDeclaration cs = new CodeTypeDeclaration(dynamicObject.Name);

            // Define base class
            if (dynamicObject.DerivedFrom is string)
            {
                cs.BaseTypes.Add(new CodeTypeReference(dynamicObject.DerivedFrom as string));
            }
            else if (dynamicObject.DerivedFrom is Type)
            {
                cs.BaseTypes.Add(new CodeTypeReference(dynamicObject.DerivedFrom as Type));
            }
            

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
