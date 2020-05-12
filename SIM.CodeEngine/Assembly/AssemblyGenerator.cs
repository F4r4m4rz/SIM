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
    internal class AssemblyGenerator
    {
        private readonly string assemblyName;
        private readonly string outputPath;
        private readonly IEnumerable<string> sourceFiles;
        private readonly IEnumerable<CodeCompileUnit> compileUnits;

        private AssemblyGenerator(string assemblyName, string outputPath)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentException("Name of target assembly is not defined", nameof(assemblyName));
            }

            if (string.IsNullOrWhiteSpace(outputPath))
            {
                throw new ArgumentException("Output path is not defined", nameof(outputPath));
            }

            this.assemblyName = assemblyName;
            this.outputPath = outputPath;
        }

        public AssemblyGenerator(string assemblyName, string outputPath, IEnumerable<string> csFiles) 
            : this(assemblyName, outputPath)
        {
            this.sourceFiles = csFiles;
        }

        public AssemblyGenerator(string assemblyName, string outputPath, IEnumerable<CodeCompileUnit> compileUnits) 
            : this(assemblyName, outputPath)
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
                return provider.CompileAssemblyFromFile(cp, sourceFiles.ToArray());

            return provider.CompileAssemblyFromDom(cp, compileUnits.ToArray());
        }

        private CompilerParameters BuildCompilerParameters()
        {
            // Build compile parameterrs
            CompilerParameters cp = new CompilerParameters();
            cp.OutputAssembly = Path.Combine(outputPath, assemblyName + ".dll");
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = false;
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("SIM.Core.dll");
            cp.ReferencedAssemblies.Add("System.ComponentModel.DataAnnotations.dll");

            return cp;
        }
    }
}
