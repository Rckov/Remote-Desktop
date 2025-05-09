using RemoteDesktop.Services.Interfaces;

namespace RemoteDesktop.ViewModels;

internal class ServerGroupModalViewModel
{
    private IDataService dataService;

    public ServerGroupModalViewModel(IDataService dataService)
    {
        this.dataService = dataService;
    }

    public string NameGroup { get; internal set; }
}