using SIM.Core.Attributes;
using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Commands
{
    [CommandString("new prop")]
    public class NewPropertyNodeCommand<T> : ISimCommand where T : PropertyNode, new()
    {
        private readonly ISimRepository repository;
        private readonly Node owner;

        public object Result { get; private set; }

        public NewPropertyNodeCommand(ISimRepository repository, Node owner)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public bool CanExecute()
        {
            // TODO: Validate possibility to create property node
            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"{GetType().Name} cannot be excuted");

            var newPropNode = new T();
            repository.Add(newPropNode);
            Result = newPropNode;
        }
    }
}
