using RemoteDesktop.Models.Base;

namespace RemoteDesktop.ViewModels.Base;

internal abstract class BaseViewModel : ObservableObject
{
    protected BaseViewModel()
    {
        InitializeCommands();
    }

    protected virtual void InitializeCommands()
    {
    }
}