﻿using SIM.CodeEngine.Assembly;
using SIM.Core.Attributes;
using SIM.Core.Commands;
using SIM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Commands
{
    [AdminCommand]
    [CommandString("compile current repos")]
    class CompileRepositoryCommand : ISimCommand
    {
        private readonly ISimRepository repository;

        public object Result => null;

        public CompileRepositoryCommand(ISimRepository repository)
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
                throw new OperationCanceledException($"{GetType().Name} cannot be excuted");

            var codeFactory = new CodeFactory(repository);
            codeFactory.BuildAssembly();
        }
    }
}