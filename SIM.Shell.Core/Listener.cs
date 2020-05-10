using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell.Core
{
    public class Listener
    {
        public event ListenerListenedEvent Listened;
        public bool ContinueListening { get; set; }
        public void Listen()
        {
            Console.Write("SIM > ");
            Listened?.Invoke(Console.ReadLine());
        }
    }
}
