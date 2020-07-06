using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Data
{
    public class DataService : ISimRepository
    {
        private static IEnumerable<ISimObject> dataSource;

        static DataService()
        {
            dataSource = new List<ISimObject>();
        }

        public void Add(ISimObject simObject)
        {
            throw new NotImplementedException();
        }

        public ISimObject Get(Func<ISimObject, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISimObject> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISimObject> GetAll(Func<ISimObject, bool> predicate)
        {
            return dataSource.Where(predicate);
        }

        public void Remove(ISimObject simObject)
        {
            throw new NotImplementedException();
        }

        public void Update(ISimObject simObject)
        {
            throw new NotImplementedException();
        }
    }
}
