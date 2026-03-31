using System;
using System.Windows;

namespace RemoteDesktop.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal class WindowAttribute(Type windowType) : Attribute
{
	public Type WindowType { get; } = typeof(Window).IsAssignableFrom(windowType)
		? windowType
		: throw new ArgumentException($"Type {windowType.Name} must inherit from Window.");
}