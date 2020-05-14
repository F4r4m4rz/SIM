using SIM.CodeEngine.Dynamic;
using SIM.Core.Interfaces;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIM.Shell.Admin
{
    public class AdminRepository : ISimRepository
    {
        private static ICollection<ISimObject> _objects;

        static AdminRepository()
        {
            _objects = new List<ISimObject>();
        }

        public AdminRepository()
        {

        }

        public AdminRepository(IEnumerable<DynamicObject> objects)
        {
            (_objects as List<ISimObject>).AddRange(objects);
        }

        public void Add(ISimObject simObject)
        {
            _objects.Add(simObject);
        }

        public ISimObject Get(Func<ISimObject, bool> predicate)
        {
            return _objects.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<ISimObject> GetAll()
        {
            return _objects;
        }

        public IEnumerable<ISimObject> GetAll(Func<ISimObject, bool> predicate)
        {
            return _objects.Where(predicate);
        }

        public void Remove(ISimObject simObject)
        {
            _objects.Remove(simObject);
        }

        public void Update(ISimObject simObject)
        {
            var @object = Get(a => a.Equals(simObject)) ??
                throw new OperationCanceledException($"{simObject} could not be found in the current repository", 
                new CancellationToken(true));

            Remove(@object);
            Add(simObject);
        }
    }
}
