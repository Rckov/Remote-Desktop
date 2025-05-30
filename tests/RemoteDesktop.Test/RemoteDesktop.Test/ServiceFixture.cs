using DryIoc;

using RemoteDesktop.Services;
using RemoteDesktop.Services.Interfaces;

using System;
using System.IO;

namespace RemoteDesktop.Test;

public class ServiceFixture : IDisposable
{
    public string DataPath { get; }
    public string SettingsPath { get; }

    public ServiceFixture()
    {
        Container = new Container();

        DataPath = Path.GetTempFileName();
        SettingsPath = Path.GetTempFileName();

        Container.RegisterDelegate<IDataService>(_ => new JsonDataService(DataPath), Reuse.Transient);
        Container.RegisterDelegate<ISettingsService>(_ => new SettingsService(SettingsPath), Reuse.Transient);
        Container.Register<IServerManagerService, ServerManagerService>(Reuse.Transient);
    }

    public IContainer Container { get; }

    public void Dispose()
    {
        if (File.Exists(DataPath))
        {
            File.Delete(DataPath);
        }

        if (File.Exists(SettingsPath))
        {
            File.Delete(SettingsPath);
        }
    }
}