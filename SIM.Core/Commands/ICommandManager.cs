using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Commands
{
    public interface ICommandManager
    {
        ICollection<Type> Commands { get; }
        Type GetCommand(string keyword);
    }
}
