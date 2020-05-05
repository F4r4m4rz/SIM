using SIM.CodeEngine.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using SIM.CodeEngine.Assembly;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicObject sdi = MakeSDI();
            DynamicObject co = MakeCO();
            Create2DirectionalRelation(sdi as DynamicNode, co as DynamicNode, out DynamicObject sdiToCo, out DynamicObject CotoSdi);

            CodeFactory codeFactory = new CodeFactory(sdi, co, sdiToCo, CotoSdi);
            var y = codeFactory.GenerateCSharpCode();
            var x = codeFactory.BuildAssembly();

            Console.Read();
        }

        static void Test()
        {
            
        }

        private static void Create2DirectionalRelation(DynamicNode sdi, DynamicNode co,
            out DynamicObject sdiToCo, out DynamicObject cotoSdi)
        {
            sdiToCo = new DynamicRelation("Relation<SDI, ControlObject>")
            {
                Name = "HasControlObject",
                Namespace = "SIM.Aibel.JSB"
            };

            var prop1 = new DynamicProperty()
            {
                IsNullable = true,
                PropertyName = "DateEstablished",
                PropertyType = DynamicPropertyType.DateTime
            };

            sdiToCo.Properties.Add(prop1);

            sdi.Relations.Add(sdiToCo as DynamicRelation);

            cotoSdi = new DynamicRelation("Relation<ControlObject, SDI>")
            {
                Name = "HasSdi",
                Namespace = "SIM.Aibel.JSB"
            };

            var prop2 = new DynamicProperty()
            {
                IsNullable = true,
                PropertyName = "DateEstablished",
                PropertyType = DynamicPropertyType.DateTime
            };

            cotoSdi.Properties.Add(prop2);

            co.Relations.Add(cotoSdi as DynamicRelation);
        }

        static DynamicObject MakeSDI()
        {
            var dynamicNode = new DynamicNode()
            {
                Name = "SDI",
                Namespace = "SIM.Aibel.JSB"
            };

            var dynamicProperty1 = new DynamicProperty()
            {
                IsNullable = true,
                PropertyName = "DataCode",
                PropertyType = DynamicPropertyType.String
            };

            var dynamicProperty2 = new DynamicProperty()
            {
                IsNullable = true,
                PropertyName = "StartDate",
                PropertyType = DynamicPropertyType.DateTime
            };

            dynamicNode.Properties.Add(dynamicProperty1);
            dynamicNode.Properties.Add(dynamicProperty2);

            return dynamicNode;
        }

        static DynamicObject MakeCO()
        {
            var dynamicNode = new DynamicNode()
            {
                Name = "ControlObject",
                Namespace = "SIM.Aibel.JSB"
            };

            var dynamicProperty1 = new DynamicProperty()
            {
                IsNullable = true,
                PropertyName = "COnumber",
                PropertyType = DynamicPropertyType.String
            };

            var dynamicProperty2 = new DynamicProperty()
            {
                IsNullable = true,
                PropertyName = "status",
                PropertyType = DynamicPropertyType.String
            };

            dynamicNode.Properties.Add(dynamicProperty1);
            dynamicNode.Properties.Add(dynamicProperty2);

            return dynamicNode;
        }
    }
}
