﻿using SIM.CodeEngine.Dynamic;
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
    [CommandString("dprop")]
    public class NewDynamicPropertyCommand : ISimCommand
    {
        private readonly ISimRepository repository;
        private readonly string nameSpace;
        private readonly string name;
        private readonly string dataType;
        private readonly string ownerObject;

        public object Result { get; private set; }

        public NewDynamicPropertyCommand(ISimRepository repository, string nameSpace, string name,
                                         string dataType, string ownerObject)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.nameSpace = nameSpace;
            this.name = name;
            this.dataType = dataType;
            this.ownerObject = ownerObject;
        }

        public bool CanExecute()
        {
            // Check if ownerObject exists in repository and put it in result
            Result = repository.Get(a => (a as DynamicObject).Name == ownerObject) as DynamicObject;
            if (Result == null) return false;

            // Check if ownerObject already has a property with this name
            if ((Result as DynamicObject).Properties.FirstOrDefault(a => a.PropertyName == name) != null)
                return false;

            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"{GetType().Name} cannot be excuted");

            (Result as DynamicObject).Properties.Add(new DynamicProperty(name, dataType)); 
        }
    }
}
