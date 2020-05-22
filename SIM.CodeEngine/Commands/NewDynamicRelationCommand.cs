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
    [CommandString("drel")]
    public class NewDynamicRelationCommand : ISimCommand
    {
        private readonly ISimRepository repository;
        private readonly string nameSpace;
        private readonly string name;
        private readonly string[] originTypes;
        private readonly string[] targetTypes;

        public object Result { get; set; }

        public NewDynamicRelationCommand(ISimRepository repository, string nameSpace, string name,
                                         string[] originTypes, string[] targetTypes)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.nameSpace = nameSpace;
            this.name = name;
            this.originTypes = originTypes;
            this.targetTypes = targetTypes;
        }

        public bool CanExecute()
        {
            // Check if origin and target types exist in the repository
            //if (repository.Get(a => (a as DynamicObject).Name == originType) == null ||
            //    repository.Get(a => (a as DynamicNode).Name == targetType) == null)
            //    return false;

            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"{typeof(NewDynamicRelationCommand).Name} cannot be excuted");

            Result = new DynamicRelation(nameSpace, name, originTypes, targetTypes);
            repository.Add(Result as ISimObject);
        }
    }
}
