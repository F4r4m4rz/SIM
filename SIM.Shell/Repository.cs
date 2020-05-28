using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell
{
    public class Repository : ISimRepository
    {
        private static ICollection<ISimObject> objects;

        public IList<Node> Nodes => GetAll(c => c is Node).Select(c => c as Node).ToList().AsReadOnly();

        public IList<Relation> Relations => GetAll(c => c is Relation).Select(c => c as Relation).ToList().AsReadOnly();

        static Repository()
        {
            objects = new List<ISimObject>();
        }

        public void Add(ISimObject simObject)
        {
            // Get all relation properties
            var relationProps = simObject.GetType().GetProperties()
                .Where(a => a.PropertyType == typeof(Relation))
                .Select(a => a.GetValue(simObject) as Relation);
            relationProps.Where(a => a != null).ToList().ForEach(a => Add(a));

            objects.Add(simObject);
        }

        public ISimObject Get(Func<ISimObject, bool> predicate)
        {
            return objects.Where(predicate)
                .FirstOrDefault();
        }

        public IEnumerable<ISimObject> GetAll()
        {
            return objects;
        }

        public IEnumerable<ISimObject> GetAll(Func<ISimObject, bool> predicate)
        {
            return objects.Where(predicate);
        }

        public void Remove(ISimObject simObject)
        {
            objects.Remove(simObject);
        }

        public void Update(ISimObject simObject)
        {
            throw new NotImplementedException();
        }
    }
}
