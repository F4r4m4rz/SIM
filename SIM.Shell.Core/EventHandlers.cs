using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell.Core
{
    public delegate void ListenerListenedEvent(string userInput);
    public delegate void NotValidCommandEvent(CommandAnalyser analyser);
}
