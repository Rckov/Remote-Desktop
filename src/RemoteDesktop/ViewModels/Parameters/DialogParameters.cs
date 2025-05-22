using System.Collections.Generic;

namespace RemoteDesktop.ViewModels.Parameters;

internal class InputData<T>(T value = default, IEnumerable<string> names = default)
{
    public T Value { get; } = value;
    public IEnumerable<string> Names { get; } = names;
}