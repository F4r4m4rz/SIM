using SIM.CodeEngine.Dynamic;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Assembly
{
    public enum outputPathOptions { CustomPath, DefaultPath}

    public class CodeFactory
    {
        private readonly IEnumerable<DynamicObject> dynamicObjects;
        private bool codeIsGenerated = false;
        private ICollection<string> csFiles;

        private CodeFactory()
        {
            csFiles = new List<string>();
        }

        public CodeFactory(IEnumerable<DynamicObject> dynamicObjects) : this()
        {
            this.dynamicObjects = dynamicObjects ?? throw new ArgumentNullException(nameof(dynamicObjects));
        }

        public CodeFactory(params DynamicObject[] dynamicObjects) : this(dynamicObjects.AsEnumerable())
        {

        }

        public string GenerateCSharpCode()
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string outputPath = Path.Combine(appdataPath, "SIM", "Auto generated codes",
                                             dynamicObjects.First().Namespace);
            GenerateCSharpCode(outputPath);
            return outputPath;
        }

        public void GenerateCSharpCode(string outputPath)
        {
            for (int i = 0; i < dynamicObjects.Count(); i++)
            {
                csFiles.Add(GenerateCSharpCode(dynamicObjects.ElementAt(i), outputPath));
            }

            codeIsGenerated = true;
        }

        private string GenerateCSharpCode(DynamicObject dynamicObject, string outputPath)
        {
            CSharpCodeGenerator codeGenerator = new CSharpCodeGenerator(dynamicObject, outputPath);
            return codeGenerator.GenerateCode(true);
        }

        private IEnumerable<CodeCompileUnit> GenerateCSharpCodeAsCompileUnit()
        {
            var result = new List<CodeCompileUnit>();

            for (int i = 0; i < dynamicObjects.Count(); i++)
            {
                result.Add(GenerateCSharpCodeAsCompileUnit(dynamicObjects.ElementAt(i)));
            }

            return result;
        }

        private CodeCompileUnit GenerateCSharpCodeAsCompileUnit(DynamicObject dynamicObject)
        {
            CSharpCodeGenerator codeGenerator = new CSharpCodeGenerator(dynamicObject);
            return codeGenerator.GenerateCode(false);
        }

        private void ValidateOutputPath(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private string GenerateDefaultAssemblyOutputPath()
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appdataPath, "SIM", "Auto generated assemblies");
        }

        public string BuildAssembly()
        {
            string outputPath = GenerateDefaultAssemblyOutputPath();
            string assemblyName = dynamicObjects.First().Namespace;
            return BuildAssembly(assemblyName, outputPath);
        }

        public string BuildAssembly(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentException("Assembly file name is not defined", nameof(assemblyName));
            }
            string outputPath = GenerateDefaultAssemblyOutputPath();
            return BuildAssembly(assemblyName, outputPath);
        }

        public string BuildAssembly(string assemblyName, string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
            {
                throw new ArgumentException("Output path for assembly is not defined", nameof(outputPath));
            }

            ValidateOutputPath(outputPath);

            if (!codeIsGenerated)
                return BuildAssembly(GenerateCSharpCodeAsCompileUnit(), assemblyName, outputPath);

            return BuildAssemblyFromFiles(assemblyName, outputPath);
        }

        private string BuildAssemblyFromFiles(string assemblyName, string outputPath)
        {
            AssemblyGenerator generator = new AssemblyGenerator(assemblyName, outputPath, csFiles);
            var result = generator.Complie();
            return Path.Combine(outputPath, assemblyName + ".dll");
        }

        private string BuildAssembly(IEnumerable<CodeCompileUnit> codeCompileUnits, 
                                     string assemblyName, string outputPath)
        {
            AssemblyGenerator generator = new AssemblyGenerator(assemblyName, outputPath, codeCompileUnits);
            var result = generator.Complie();
            return Path.Combine(outputPath, assemblyName + ".dll");
        }
    }
}
