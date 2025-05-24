namespace RemoteDesktop.Models.Messages;

/// <summary>
/// Message carrying a value of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">Type of the value.</typeparam>
public class ValueMessage<T>(T value)
{
    /// <summary>
    /// The value carried by the message.
    /// </summary>
    public T Value { get; } = value;
}

/// <summary>
/// Message for value changes of type <typeparamref name="T"/>.
/// Includes the change action and old value.
/// </summary>
/// <typeparam name="T">Type of the value.</typeparam>
public class ChangeValueMessage<T>(ChangeAction action, T newValue, T oldValue = default) : ValueMessage<T>(newValue)
{
    /// <summary>
    /// The previous value before the change.
    /// </summary>
    public T OldValue { get; } = oldValue;

    /// <summary>
    /// The type of change (Create or Update).
    /// </summary>
    public ChangeAction Action { get; } = action;
}

/// <summary>
/// Types of actions for <see cref="ChangeValueMessage{T}"/>.
/// </summary>
public enum ChangeAction
{
    /// <summary>
    /// A new value was created.
    /// </summary>
    Create,

    /// <summary>
    /// An existing value was updated.
    /// </summary>
    Update
}