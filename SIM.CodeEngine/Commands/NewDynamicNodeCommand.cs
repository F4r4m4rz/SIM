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

namespace SIM.CodeEngine.Commands
{
    [AdminCommand]
    [CommandString("dnode")]
    public class NewDynamicNodeCommand : ISimCommand
    {
        private readonly ISimRepository repository;
        private readonly string nameSpace;
        private readonly string name;

        public object Result { get; set; }

        public NewDynamicNodeCommand(ISimRepository repository, string nameSpace, string name)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.nameSpace = nameSpace;
            this.name = name;
        }

        public bool CanExecute()
        {
            // Check if repository contains any node with the passed name
            if (repository.Get(a => (a as DynamicObject).Name == name) != null)
                return false;

            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException();

            Result = new DynamicNode(nameSpace, name);
            repository.Add(Result as ISimObject);
        }
    }
}
