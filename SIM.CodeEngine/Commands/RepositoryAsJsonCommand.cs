using SIM.Core.Attributes;
using SIM.Core.Commands;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SIM.DataBase;

namespace SIM.CodeEngine.Commands
{
    [AdminCommand]
    [CommandString("as json current repos")]
    public class RepositoryAsJsonCommand : ISimCommand
    {
        private readonly ISimRepository repository;
        private readonly string nameSpace;

        public object Result => null;

        public RepositoryAsJsonCommand(ISimRepository repository, string nameSpace)
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
            TextWriter writer = new StreamWriter(path);
            JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            var text = JsonConvert.SerializeObject(repository.GetAll(), settings);
            writer.Write(text);
            writer.Close();
        }
    }
}
