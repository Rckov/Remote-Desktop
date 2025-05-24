using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.Generic;

namespace RemoteDesktop.Services;

/// <summary>
/// Messenger service class. Stores subscribers using weak references.
/// </summary>
internal class MessengerService : IMessengerService
{
    private readonly IDictionary<Type, IList<WeakReference>> _handlers = new Dictionary<Type, IList<WeakReference>>();

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void Send<TMessage>(TMessage message)
    {
        if (_handlers.TryGetValue(typeof(TMessage), out var handlers))
        {
            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                if (handlers[i].Target is Action<TMessage> handler)
                {
                    handler(message);
                }
                else
                {
                    handlers.RemoveAt(i);
                }
            }
        }
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void Subscribe<TMessage>(Action<TMessage> handler)
    {
        var messageType = typeof(TMessage);

        if (!_handlers.TryGetValue(messageType, out var handlers))
        {
            handlers = [];
            _handlers[messageType] = handlers;
        }

        handlers.Add(new WeakReference(handler));
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public void Unsubscribe<TMessage>(Action<TMessage> handler)
    {
        if (_handlers.TryGetValue(typeof(TMessage), out var handlers))
        {
            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                var target = handlers[i].Target;

                if (target == null || ReferenceEquals(target, handler))
                {
                    handlers.RemoveAt(i);
                }
            }
        }
    }
}