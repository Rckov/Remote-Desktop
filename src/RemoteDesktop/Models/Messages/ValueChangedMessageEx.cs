using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RemoteDesktop.Models.Messages;

public enum ChangeAction
{
    Create,
    Update,
}

public class ValueChangedMessageEx<T>(ChangeAction action, T newValue, T oldValue = default) : ValueChangedMessage<T>(newValue)
{
    public T OldValue { get; } = oldValue;
    public ChangeAction Action { get; } = action;
}