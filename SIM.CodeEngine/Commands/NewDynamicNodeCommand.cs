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
        private readonly bool isVisible;

        public object Result { get; set; }

        public NewDynamicNodeCommand(ISimRepository repository, string nameSpace, string name, bool isVisible)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.nameSpace = nameSpace;
            this.name = name;
            this.isVisible = isVisible;
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

            Result = new DynamicNode(nameSpace, name, isVisible);
            repository.Add(Result as ISimObject);
        }
    }
}
