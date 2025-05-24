using DryIoc.ImTools;

using RemoteDesktop.Common;
using RemoteDesktop.Common.Base;
using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RemoteDesktop.ViewModels;

internal class MainViewModel : BaseViewModel
{
    private readonly IWindowService _windowService;
    private readonly IMessengerService _messengerService;
    private readonly INotificationService _notificationService;
    private readonly IDataService _dataService;

    public MainViewModel(
        IWindowService windowService,
        IMessengerService messengerService,
        INotificationService notificationService,
        IDataService dataService)
    {
        _windowService = windowService;
        _messengerService = messengerService;
        _notificationService = notificationService;
        _dataService = dataService;
    }

    public string SearchText
    {
        get;
        set => OnSearchTextChanged(field, value);
    }

    public ObservableCollection<TreeItemViewModel> ServersGroups { get; set; }

    public ICommand ConnectCommand { get; private set; }
    public ICommand CreateServerCommand { get; private set; }
    public ICommand CreateServerGroupCommand { get; private set; }
    public ICommand EditCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }

    public override void InitializeCommands()
    {
        ConnectCommand = new RelayCommand(Connect);
        CreateServerCommand = new RelayCommand(CreateServer);
        CreateServerGroupCommand = new RelayCommand(CreateGroup);
        EditCommand = new RelayCommand(Edit);
        DeleteCommand = new RelayCommand(Delete);
    }

    private void Connect()
    {
        throw new NotImplementedException();
    }

    private void CreateServer()
        => CreateOrUpdateModel<Server, ServerViewModel>();

    private void CreateGroup()
        => CreateOrUpdateModel<ServerGroup, ServerGroupViewModel>();

    private void Edit()
    {
        throw new NotImplementedException();
    }

    private void Delete()
    {
        throw new NotImplementedException();
    }

    private void OnSearchTextChanged(string fieldValue, string value)
    {
        if (!Set(ref fieldValue, value))
        {
            return;
        }

        foreach (var item in ServersGroups)
        {
            item.ApplyFilter(value);
        }
    }


    private bool CreateOrUpdateModel<T, TViewModel>(T model = default) where TViewModel : class
    {
        //var list = ServersGroups.GetNames();
        //var data = new InputData<T>(model, list);

        return _windowService.ShowDialog<TViewModel>() == true;
    }
}