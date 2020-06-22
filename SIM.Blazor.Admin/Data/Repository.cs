using SIM.Blazor.Admin.Shared;
using SIM.CodeEngine.Commands;
using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Admin.Data
{
    public class Repository : ISimRepository
    {
        public static ICollection<ISimObject> objects;
        public Repository()
        {
            objects = new List<ISimObject>();
        }
        public void Add(ISimObject simObject)
        {
            objects.Add(simObject);
        }

        public ISimObject Get(Func<ISimObject, bool> predicate)
        {
            return objects.Where(predicate).FirstOrDefault();
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
