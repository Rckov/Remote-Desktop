using System;

namespace RemoteDesktop.Services.Interfaces;

/// <summary>
/// Interface for a messaging service. Allows sending messages and managing subscriptions.
/// </summary>
public interface IMessengerService
{
    /// <summary>
    /// Sends a message of the specified type.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="message">The message to send.</param>
    void Send<TMessage>(TMessage message);

    /// <summary>
    /// Subscribes a handler to messages of the specified type.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="handler">The message handler.</param>
    void Subscribe<TMessage>(Action<TMessage> handler);

    /// <summary>
    /// Unsubscribes a handler from messages of the specified type.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <param name="handler">The handler to remove.</param>
    void Unsubscribe<TMessage>(Action<TMessage> handler);
}