using System;

namespace SIM.Blazor.Admin.Data
{
    public interface IMediator
    {
        event EventHandler<string> ContextUpdated;
        string Context { get; set; }
    }
}