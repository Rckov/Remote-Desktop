using RemoteDesktop.Models.Base;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace RemoteDesktop.Models;

[DataContract]
internal class ServerGroup : ObservableObject
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public ObservableCollection<Server> Servers { get; set; } = [];
}