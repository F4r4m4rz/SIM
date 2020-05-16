using SIM.Core.Attributes;
using SIM.Core.Interfaces;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Commands
{
    [CommandString("new relation")]
    public class NewRelationCommand<T> : ISimCommand where T : Relation, new()
    {
        private readonly ISimRepository repository;
        private readonly Node origin;
        private readonly Node target;

        public object Result { get; private set; }

        public NewRelationCommand(ISimRepository repository, Node origin, Node target)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.origin = origin ?? throw new ArgumentNullException(nameof(origin));
            this.target = target ?? throw new ArgumentNullException(nameof(target));
        }

        public bool CanExecute()
        {
            // TODO: Check if the request relation type can 
            //       be created between presented nodes
            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"{GetType().Name} cannot be excuted");

            var newRelation = new T() { Origin = origin, Target = target };
            repository.Add(newRelation);
            Result = newRelation;
        }
    }
}
