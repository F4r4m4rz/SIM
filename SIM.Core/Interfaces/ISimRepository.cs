using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Interfaces
{
    public interface ISimRepository
    {
        void Add(ISimObject simObject);
        void Remove(ISimObject simObject);
        void Update(ISimObject simObject);
        ISimObject Get(Func<ISimObject, bool> predicate);
        IEnumerable<ISimObject> GetAll();
        IEnumerable<ISimObject> GetAll(Func<ISimObject, bool> predicate);
    }
}
