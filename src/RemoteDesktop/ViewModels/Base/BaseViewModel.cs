using RemoteDesktop.Models.Base;

using System.Runtime.Serialization;

namespace RemoteDesktop.ViewModels.Base;

[DataContract]
internal class BaseViewModel : ObservableObject
{
    protected BaseViewModel()
    {
        InitializeCommands();
    }

    protected virtual void InitializeCommands()
    {
    }
}