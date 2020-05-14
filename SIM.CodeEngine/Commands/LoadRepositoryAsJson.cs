﻿using SIM.Core.Commands;
using SIM.Core.Interfaces;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SIM.CodeEngine.Dynamic;
using SIM.Core.Attributes;
using SIM.Core.Objects;
using System.Collections;

namespace SIM.CodeEngine.Commands
{
    [AdminCommand]
    [CommandString("load json")]
    public class LoadRepositoryAsJson : ISimCommand
    {
        private readonly ISimRepository repository;
        private readonly string nameSpace;

        public object Result { get; private set; }

        public LoadRepositoryAsJson(ISimRepository repository, string nameSpace)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.nameSpace = nameSpace;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"{GetType().Name} cannot be excuted");

            JsonSerializer serializer = new JsonSerializer();
            StreamReader reader = new StreamReader($@"C:\Users\ofsfabo1\AppData\Roaming\SIM\Json\{nameSpace}.json");
            Result = serializer.Deserialize(reader, typeof(IEnumerable<DynamicObject>));
            reader.Close();
        }
    }
}