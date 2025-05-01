using AvalonDock.Themes;

using RemoteDesktop.Models.Base;
using RemoteDesktop.Services.Implementation;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace RemoteDesktop.Models;

[DataContract]
internal class Settings
{
    [DataMember]
    public ThemeType ThemeType { get; set; }
}