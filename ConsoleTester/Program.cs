using SIM.CodeEngine.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using SIM.CodeEngine.Assembly;
using SIM.Aibel;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var dynamicNode = new DynamicNode()
            {
                Name = "SDI",
                Namespace = "SIM.Aibel"
            };

            var dynamicProperty = new DynamicProperty()
            {
                IsNullable = true,
                PropertyName = "DataCode",
                PropertyType = DynamicPropertyType.String
            };

            dynamicNode.Properties.Add(dynamicProperty);

            // json serializerr
            //JsonSerializer serializer = new JsonSerializer();
            //using (StreamWriter sw = new StreamWriter(@"c:\test\test.json"))
            //{
            //    serializer.Serialize(sw, dynamicNode);
            //}

            //CSharpCodeGenerator code = new CSharpCodeGenerator(dynamicNode);
            //Console.WriteLine(code.GenerateCode(false));

            //AssemblyGenerator ag = new AssemblyGenerator("Test", code.GenerateCode(false));
            //var x = ag.Complie();

            SDI sdi = new SDI();
            sdi.DataCode = "Hello world!";

            Console.Read();
        }
    }
}
