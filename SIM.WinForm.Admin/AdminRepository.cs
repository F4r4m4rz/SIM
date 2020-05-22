using SIM.CodeEngine.Dynamic;
using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.WinForm.Admin
{
    public class AdminRepository : ISimRepository
    {
        public static readonly IList<ISimObject> _db;
        public IList<DynamicNode> Nodes => GetAll(c=>c is DynamicNode).Select(c=>c as DynamicNode).ToList();

        public IList<DynamicRelation> Relations => GetAll(c => c is DynamicRelation).Select(c => c as DynamicRelation).ToList();

        static AdminRepository()
        {
            _db = new List<ISimObject>();
        }

        public void Add(ISimObject simObject)
        {
            _db.Add(simObject);
        }

        public ISimObject Get(Func<ISimObject, bool> predicate)
        {
            return _db.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<ISimObject> GetAll()
        {
            return _db;
        }

        public IEnumerable<ISimObject> GetAll(Func<ISimObject, bool> predicate)
        {
            return _db.Where(predicate);
        }

        public void Remove(ISimObject simObject)
        {
            _db.Remove(simObject);
        }

        public void Update(ISimObject simObject)
        {
            Remove(simObject);
            Add(simObject);
        }
    }
}
