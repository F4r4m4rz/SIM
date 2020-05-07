using SIM.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Commands
{
    public interface ISimPropertySetCommand
    {
        bool CanExecute(ISimObject obj, PropertyInfo prop);
        void Execute(ISimObject obj, PropertyInfo prop);
    }
}
