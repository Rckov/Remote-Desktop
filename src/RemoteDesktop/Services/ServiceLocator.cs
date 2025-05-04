using System;
using System.Collections.Generic;

namespace RemoteDesktop.Services;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = [];

    public static void Register<TService>(TService implementation) where TService : class
    {
        var type = typeof(TService);

        if (_services.ContainsKey(type))
        {
            _services[type] = implementation;
        }
        else
        {
            _services.Add(type, implementation);
        }
    }

    public static TService Get<TService>() where TService : class
    {
        var type = typeof(TService);

        if (_services.TryGetValue(type, out var implementation))
        {
            return implementation as TService;
        }

        throw new InvalidOperationException($"Service of type {type.Name} is not registered.");
    }
}