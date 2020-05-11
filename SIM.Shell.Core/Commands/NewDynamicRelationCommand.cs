using SIM.CodeEngine.Dynamic;
using SIM.Core.Attributes;
using SIM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell.Core.Commands
{
    [AdminCommand]
    [CommandString("drel")]
    public class NewDynamicRelationCommand : ISimCommand
    {
        private readonly string nameSpace;
        private readonly string name;
        private readonly string originType;
        private readonly string targetType;

        public object Result { get; set; }

        public NewDynamicRelationCommand(string nameSpace, string name, string originType, string targetType)
        {
            this.nameSpace = nameSpace;
            this.name = name;
            this.originType = originType;
            this.targetType = targetType;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"{typeof(NewDynamicRelationCommand).Name} cannot be excuted");

            Result = new DynamicRelation(nameSpace, name, originType, targetType);
        }
    }
}
