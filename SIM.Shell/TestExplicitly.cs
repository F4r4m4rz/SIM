//using SIM.Aibel.JSB;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell
{
    public class TestExplicitly
    {
        //public TestExplicitly()
        //{
        //    var factory = new SimNodeFactory();
        //    var repository = new Repository();
        //    SDI s = new SDI() { HasSection = new HasSection() };
        //    var rela = new PropertyRelation() { Origin = s, Target = new DateTimePropertyNode(DateTime.Now) };
        //    s.IssuedDate = rela;
        //    Validator.ValidateObject(s, new ValidationContext(s), true);
        //    Section section = NewSection(factory);
        //    repository.Add(section);
        //    SDI sdi = NewSdi(factory, section);
        //    repository.Add(sdi);
        //    CO co = NewCO(factory, section, sdi);
        //    repository.Add(co);
        //    var rel = sdi.RelateTo<HasSection>(co, true);
        //    repository.Add(rel);
        //}

        //private CO NewCO(SimNodeFactory factory, Section section, SDI sdi)
        //{
        //    var args = factory.GetConstructionArguments<CO>();
        //    args.AssignArgumentValues(new StringPropertyNode("=12315/56540"), null);
        //    return factory.New(args);
        //}

        //private Section NewSection(SimNodeFactory factory)
        //{
        //    var args = factory.GetConstructionArguments<Section>();
        //    return factory.New(args);
        //}

        //private SDI NewSdi(SimNodeFactory factory, Section section)
        //{
        //    var args = factory.GetConstructionArguments<SDI>();
        //    args.AssignArgumentValues(section, new DateTimePropertyNode(DateTime.Now));
        //    return factory.New<SDI>(args);
        //}
    }
}
