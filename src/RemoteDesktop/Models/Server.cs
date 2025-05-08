using RemoteDesktop.Models.Base;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace RemoteDesktop.Models;

[DataContract]
internal class ServerGroup : ObservableObject
{
    [DataMember]
    public string Name
    {
        get;
        set => Set(ref field, value);
    }

    [DataMember]
    public ObservableCollection<Server> Servers { get; set; } = [];
}

[DataContract]
internal class Server : ObservableObject
{
    [DataMember]
    public string Name
    {
        get;
        set => Set(ref field, value);
    }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public string Host { get; set; }

    [DataMember]
    public string Username { get; set; }

    [DataMember]
    public string Password { get; set; }

    [DataMember]
    public int Port { get; set; }
}