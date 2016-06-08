using System;

namespace Demo.SqlConnections.WinForm
{
    public interface ISourceGatherer
    {
        string GetSourceFor(Type type);
    }
}