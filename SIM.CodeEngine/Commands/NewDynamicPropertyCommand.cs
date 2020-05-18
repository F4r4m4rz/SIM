using SIM.CodeEngine.Dynamic;
using SIM.Core.Attributes;
using SIM.Core.Commands;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Commands
{
    [AdminCommand]
    [CommandString("dprop")]
    public class NewDynamicPropertyCommand : ISimCommand
    {
        private readonly ISimRepository repository;
        private readonly string nameSpace;
        private readonly string name;
        private readonly string dataType;
        private readonly bool isRequired;
        private readonly bool isUserInput;
        private readonly string ownerObject;

        public object Result { get; private set; }

        public NewDynamicPropertyCommand(ISimRepository repository, string nameSpace, string name,
                                         string ownerObject, string dataType, bool isRequired, bool isUserInput)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.nameSpace = nameSpace;
            this.name = name;
            this.dataType = dataType;
            this.isRequired = isRequired;
            this.isUserInput = isUserInput;
            this.ownerObject = ownerObject;
        }

        public bool CanExecute()
        {
            // Check if ownerObject exists in repository and put it in result
            Result = repository.Get(a => (a as DynamicNode).Name == ownerObject) as DynamicNode;
            if (Result == null) return false;

            // Check if ownerObject already has a property with this name
            if ((Result as DynamicNode).Properties.FirstOrDefault(a => a.Name == name) != null)
                return false;

            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"{GetType().Name} cannot be excuted");

            (Result as DynamicNode).Properties.Add(new DynamicProperty(nameSpace, name, dataType, isRequired, isUserInput)); 
        }
    }
}
