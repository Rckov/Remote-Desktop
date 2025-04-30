using RemoteDesktop.Models.Base;

using System.Runtime.Serialization;

namespace RemoteDesktop.Models;

[DataContract]
internal class Server : ObservableObject
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public int IdGroup { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public string Host { get; set; }

    [DataMember]
    public string Password { get; set; }

    [DataMember]
    public int Port { get; set; }
}