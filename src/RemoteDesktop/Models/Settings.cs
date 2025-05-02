using RemoteDesktop.Services.Implementation;

using System.Runtime.Serialization;

namespace RemoteDesktop.Models;

[DataContract]
internal class Settings
{
    [DataMember]
    public ThemeType ThemeType { get; set; }
}