﻿using SIM.Core.Attributes;
using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIM.Core.Commands
{
    [CommandString("new node")]
    public class NewNodeCommand<T> : ISimCommand where T : Node, new()
    {
        private readonly ISimRepository repository;

        public object Result { get; set; }

        public NewNodeCommand(ISimRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"Cannot create new instance of{typeof(T)}",
                                                 new CancellationToken(true));

            var newNode = new T();
            repository.Add(newNode);
            Result = newNode;
        }
    }
}
