using Bogus;

using DryIoc;

using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System.Collections.ObjectModel;
using System.Linq;

using Xunit;

namespace RemoteDesktop.Test.Services;

public class ServerManagerServiceTests : IClassFixture<ServiceFixture>
{
    private readonly IServerManagerService _managerService;

    private readonly Faker<Server> _serverFaker;
    private readonly Faker<ServerGroup> _groupFaker;

    public ServerManagerServiceTests(ServiceFixture fixture)
    {
        _managerService = fixture.Container.Resolve<IServerManagerService>();

        _serverFaker = new Faker<Server>()
            .RuleFor(s => s.Name, f => f.Internet.DomainName())
            .RuleFor(s => s.Description, f => f.Lorem.Sentence())
            .RuleFor(s => s.Host, f => f.Internet.Ip())
            .RuleFor(s => s.Username, f => f.Internet.UserName())
            .RuleFor(s => s.Password, f => f.Internet.Password())
            .RuleFor(s => s.Port, f => f.Internet.Port())
            .RuleFor(s => s.GroupName, f => f.Commerce.Department());

        _groupFaker = new Faker<ServerGroup>()
            .RuleFor(g => g.Name, f => f.Commerce.Department())
            .RuleFor(g => g.Description, f => f.Lorem.Sentence())
            .RuleFor(g => g.Servers, f => new ObservableCollection<Server>(_serverFaker.Generate(3)));
    }

    [Fact(DisplayName = "AddGroup should add a new group")]
    public void Test0()
    {
        // Arrange
        var group = _groupFaker.Generate();

        // Act
        _managerService.AddGroup(group);
        var groups = _managerService.LoadData();

        // Assert
        Assert.Contains(groups, g => g.Name == group.Name);
    }

    [Fact(DisplayName = "DeleteGroup should remove a group")]
    public void Test1()
    {
        // Arrange
        var group = _groupFaker.Generate();
        _managerService.AddGroup(group);

        // Act
        _managerService.DeleteGroup(group);
        var groups = _managerService.LoadData();

        // Assert
        Assert.DoesNotContain(groups, g => g.Name == group.Name);
    }

    [Fact(DisplayName = "AddServer should add server to specified group")]
    public void Test2()
    {
        // Arrange
        var group = _groupFaker.Generate();
        _managerService.AddGroup(group);

        var server = _serverFaker.Generate();
        server.GroupName = group.Name;

        // Act
        _managerService.AddServer(server, group.Name);
        var loadedGroup = _managerService.LoadData().First(g => g.Name == group.Name);

        // Assert
        Assert.Contains(loadedGroup.Servers, s => s.Name == server.Name);
    }

    [Fact(DisplayName = "DeleteServer should remove server from group")]
    public void Test3()
    {
        // Arrange
        var group = _groupFaker.Generate();
        var server = _serverFaker.Generate();

        server.GroupName = group.Name;
        group.Servers.Add(server);

        _managerService.AddGroup(group);

        // Act
        _managerService.DeleteServer(server);
        var loadedGroup = _managerService.LoadData().First(g => g.Name == group.Name);

        // Assert
        Assert.DoesNotContain(loadedGroup.Servers, s => s.Name == server.Name);
    }

    [Fact(DisplayName = "UpdateGroup should update server GroupName when name changed")]
    public void Test4()
    {
        // Arrange
        var group = _groupFaker.Generate();
        group.Servers = new ObservableCollection<Server>(_serverFaker.Generate(3));

        foreach (var server in group.Servers)
        {
            server.GroupName = group.Name;
        }

        _managerService.AddGroup(group);

        var oldName = group.Name;
        group.Name = oldName + "_Renamed";

        // Act
        _managerService.UpdateGroup(group, oldName);

        // Assert
        Assert.All(group.Servers, s => Assert.Equal(group.Name, s.GroupName));
    }

    [Fact(DisplayName = "MoveServer should move server to another group")]
    public void Test5()
    {
        // Arrange
        var group1 = _groupFaker.Generate();
        var group2 = _groupFaker.Generate();

        var server = _serverFaker.Generate();
        server.GroupName = group1.Name;
        group1.Servers.Add(server);

        _managerService.AddGroup(group1);
        _managerService.AddGroup(group2);

        // Act
        _managerService.MoveServer(server, group2.Name);

        var loadedGroup1 = _managerService.LoadData().First(g => g.Name == group1.Name);
        var loadedGroup2 = _managerService.LoadData().First(g => g.Name == group2.Name);

        // Assert
        Assert.DoesNotContain(loadedGroup1.Servers, s => s.Name == server.Name);
        Assert.Contains(loadedGroup2.Servers, s => s.Name == server.Name);
    }

    [Fact(DisplayName = "SaveData should persist current state")]
    public void Test6()
    {
        // Arrange
        var group = _groupFaker.Generate();

        foreach (var item in group.Servers)
        {
            item.GroupName = group.Name;
        }

        _managerService.AddGroup(group);
        _managerService.SaveData();

        // Act
        var loadedGroups = _managerService.LoadData().ToList();

        // Assert
        Assert.Single(loadedGroups);
        Assert.Equal(group.Name, loadedGroups[0].Name);
        Assert.Equal(group.Servers.Count, loadedGroups[0].Servers.Count);
    }
}