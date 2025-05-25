using System.Collections.Generic;

namespace RemoteDesktop.Common.Parameters;

/// <summary>
/// Container for passing parameters between ViewModels.
/// </summary>
internal class InputData<T>(T value = default, IEnumerable<string> names = default)
{
    public T Value { get; } = value;
    public IEnumerable<string> Names { get; } = names;
}