using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public class Graph : ISimObject
    {
        public Graph()
        {
            Nodes = new List<Node>();
            Relations = new List<Relation>();
        }
        public ICollection<Node> Nodes { get; set; }
        public ICollection<Relation> Relations { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
