using DryIoc;

using RemoteDesktop.Services;
using RemoteDesktop.Services.Interfaces;

using System;
using System.IO;

namespace RemoteDesktop.Test;

public class ServiceFixture : IDisposable
{
    private string _dataPath;

    public ServiceFixture()
    {
        Container = new Container();
        Container.RegisterDelegate<IDataService>(x =>
        {
            _dataPath = Path.GetTempFileName();
            return new JsonDataService(_dataPath);
        }, Reuse.Transient);
    }

    public IContainer Container { get; }

    public void Dispose()
    {
        if (File.Exists(_dataPath))
        {
            File.Delete(_dataPath);
        }
    }
}