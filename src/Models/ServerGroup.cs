using CommunityToolkit.Mvvm.ComponentModel;

using System;

namespace RemoteDesktop.Models;

internal partial class ServerGroup : ObservableObject
{
	[ObservableProperty]
	private Guid _id = Guid.NewGuid();

	[ObservableProperty]
	private string _name = string.Empty;
}