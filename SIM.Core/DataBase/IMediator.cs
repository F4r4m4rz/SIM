using System;

namespace SIM.Core.DataBase
{
    public interface IMediator
    {
        event EventHandler<string> ContextUpdated;
        string Context { get; set; }
    }
}