using SIM.CodeEngine.Dynamic;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
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
        private CodeCompileUnit compileUnit;

        private CSharpCodeGenerator()
        {
            compileUnit = new CodeCompileUnit();
        }

        public CSharpCodeGenerator(DynamicObject dynamicObject) : this()
        {
            this.dynamicObject = dynamicObject ?? 
                throw new ArgumentNullException(nameof(dynamicObject));
        }

        public void GenerateCode()
        {
            // Define namespace
            Namespace();
        }

        private void Namespace()
        {
            CodeNamespace ns = new CodeNamespace(dynamicObject.Namespace);

            // import system and SIM.Core
            CodeNamespaceImport system = new CodeNamespaceImport("System");
            CodeNamespaceImport simCore = new CodeNamespaceImport("SIM.Core");
            ns.Imports.Add(system);
            ns.Imports.Add(simCore);

            // Add new namespace
            compileUnit.Namespaces.Add(ns);
        }
    }
}
