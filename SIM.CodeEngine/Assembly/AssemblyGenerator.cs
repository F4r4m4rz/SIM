using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Assembly
{
    public class AssemblyGenerator
    {
        private readonly string assemblyName;
        private readonly string[] sourceFiles;
        private readonly CodeCompileUnit[] compileUnits;

        private AssemblyGenerator(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentException("Name of target assembly is not defined", nameof(assemblyName));
            }

            this.assemblyName = assemblyName;
        }

        public AssemblyGenerator(string assemblyName, params string[] csFiles) : this(assemblyName)
        {
            this.sourceFiles = csFiles;
        }

        public AssemblyGenerator(string assemblyName, params CodeCompileUnit[] compileUnits) : this(assemblyName)
        {
            this.compileUnits = compileUnits;
        }

        public CompilerResults Complie()
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters cp = BuildCompilerParameters();
            return Compile(provider, cp);
        }

        private CompilerResults Compile(CSharpCodeProvider provider, CompilerParameters cp)
        {
            if (sourceFiles != null)
                return provider.CompileAssemblyFromFile(cp, sourceFiles);

            return provider.CompileAssemblyFromDom(cp, compileUnits);
        }

        private CompilerParameters BuildCompilerParameters()
        {
            // Build compile parameterrs
            CompilerParameters cp = new CompilerParameters();
            cp.OutputAssembly = GenerateOutputAssembly();
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = false;
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("SIM.Core.dll");

            return cp;
        }

        private string GenerateOutputAssembly()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var outputDir = Path.Combine(appData, "SIM", "Auto generated asseblies");

            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            return Path.Combine(outputDir, assemblyName + ".dll");
        }
    }
}
