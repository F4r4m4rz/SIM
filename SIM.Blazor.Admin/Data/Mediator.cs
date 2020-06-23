using SIM.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Admin.Data
{
    public class Mediator : IMediator
    {
        private string _context;

        public event EventHandler<string> ContextUpdated;

        public string Context
        {
            get
            {
                return _context;
            }

            set
            {
                _context = value;
                ContextUpdated?.Invoke(this, value);
            }
        }
    }
}
