using SIM.CodeEngine.Dynamic;
using SIM.Core.Attributes;
using SIM.Core.Commands;
using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell.Admin
{
    [AdminCommand]
    [CommandString("print current repos")]
    public class PrintCurrentRepository : ISimCommand
    {
        private readonly ISimRepository repository;

        public object Result => null;

        public PrintCurrentRepository(ISimRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            PrintNodes();
            PrintRelations();
        }

        private void PrintRelations()
        {
            var all = repository.GetAll(a => a is DynamicRelation);
        }

        private void PrintNodes()
        {
            var all = repository.GetAll(a => a is DynamicNode);
            Print(all);
        }

        private void Print(IEnumerable<ISimObject> all)
        {
            for (int i = 0; i < all.Count(); i++)
            {
                Print(all.ElementAt(i));
            }
        }

        private void Print(ISimObject simObject)
        {
            // Get type
            var type = simObject.GetType();
        }
    }
}
