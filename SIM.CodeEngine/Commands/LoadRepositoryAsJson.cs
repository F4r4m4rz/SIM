using SIM.Core.Commands;
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
using SIM.DataBase;
using Newtonsoft.Json.Linq;

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
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                $@"SIM\Json\{nameSpace}.json");
            StreamReader reader = new StreamReader(path);
            JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects };
            Result = JsonConvert.DeserializeObject<IEnumerable<DynamicObject>>(reader.ReadToEnd());
            reader.Close();
        }
    }
}
