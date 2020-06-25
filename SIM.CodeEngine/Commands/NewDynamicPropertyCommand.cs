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
        public object Result { get; private set; }

        public ISimRepository Repository { get; set; }

        public DynamicGraphObject Owner { get; set; }

        public string Name { get; set; }

        public string DataType { get; set; }

        public bool IsRequired { get; set; }

        public bool IsUserInput { get; set; }

        public NewDynamicPropertyCommand()
        {

        }

        public NewDynamicPropertyCommand(ISimRepository repository, DynamicGraphObject owner, string name,
                                         string dataType, bool isRequired,
                                         bool isUserInput)
        {
            this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            this.Name = name;
            this.DataType = dataType;
            this.IsRequired = isRequired;
            this.IsUserInput = isUserInput;
        }

        public bool CanExecute()
        {
            // Check if ownerObject already has a property with this name
            if (Owner.Properties.FirstOrDefault(a => a.Name == Name) != null)
                return false;

            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"{GetType().Name} cannot be excuted");

            Result = new DynamicProperty(Owner.Namespace, Name, DataType, IsRequired, IsUserInput, Repository);
            Owner.Properties.Add(Result as DynamicProperty);
        }
    }
}
