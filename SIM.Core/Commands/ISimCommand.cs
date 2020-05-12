using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Commands
{
    public interface ISimCommand
    {
        object Result { get; }
        bool CanExecute();
        void Execute();
    }
}
