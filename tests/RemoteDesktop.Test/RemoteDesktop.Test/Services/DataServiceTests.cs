using Bogus;

using DryIoc;

using RemoteDesktop.Models;
using RemoteDesktop.Services.Interfaces;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xunit;

namespace RemoteDesktop.Test.Services;

public class DataServiceTests : IClassFixture<ServiceFixture>
{
    private readonly IDataService _dataService;

    private readonly Faker<Server> _serverFaker;
    private readonly Faker<ServerGroup> _groupFaker;

    public DataServiceTests(ServiceFixture fixture)
    {
        _dataService = fixture.Container.Resolve<IDataService>();

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

    [Fact(DisplayName = "Save and Load should handle empty collections")]
    public void Test0()
    {
        // Arrange
        var emptyGroups = new List<ServerGroup>();

        // Act
        _dataService.Save(emptyGroups);
        var loadedGroups = _dataService.Load().ToList();

        // Assert
        Assert.Empty(loadedGroups);
    }

    [Fact(DisplayName = "Save and Load should handle a single group correctly")]
    public void Test1()
    {
        // Arrange
        var group = _groupFaker.Generate();

        // Act
        _dataService.Save([group]);
        var loadedGroups = _dataService.Load().ToList();

        // Assert
        Assert.Single(loadedGroups);
        Assert.Equal(group.Name, loadedGroups[0].Name);
        Assert.Equal(group.Description, loadedGroups[0].Description);
        Assert.Equal(group.Servers.Count, loadedGroups[0].Servers.Count);
    }

    [Fact(DisplayName = "Save and Load should handle multiple groups correctly")]
    public void Test2()
    {
        // Arrange
        var groups = _groupFaker.Generate(3).ToList();

        // Act
        _dataService.Save(groups);
        var loadedGroups = _dataService.Load().ToList();

        // Assert
        Assert.Equal(groups.Count, loadedGroups.Count);
        for (int i = 0; i < groups.Count; i++)
        {
            Assert.Equal(groups[i].Name, loadedGroups[i].Name);
            Assert.Equal(groups[i].Description, loadedGroups[i].Description);
            Assert.Equal(groups[i].Servers.Count, loadedGroups[i].Servers.Count);
        }
    }

    [Fact(DisplayName = "Save and Load should handle server details correctly")]
    public void Test3()
    {
        // Arrange
        var group = _groupFaker.Generate();

        // Act
        _dataService.Save([group]);
        var loadedGroups = _dataService.Load().ToList();

        // Assert
        var loadedGroup = loadedGroups[0];
        for (int i = 0; i < group.Servers.Count; i++)
        {
            Assert.Equal(group.Servers[i].Name, loadedGroup.Servers[i].Name);
            Assert.Equal(group.Servers[i].Description, loadedGroup.Servers[i].Description);
            Assert.Equal(group.Servers[i].Host, loadedGroup.Servers[i].Host);
            Assert.Equal(group.Servers[i].Username, loadedGroup.Servers[i].Username);
            Assert.Equal(group.Servers[i].Password, loadedGroup.Servers[i].Password);
            Assert.Equal(group.Servers[i].Port, loadedGroup.Servers[i].Port);
            Assert.Equal(group.Servers[i].GroupName, loadedGroup.Servers[i].GroupName);
        }
    }

    [Fact(DisplayName = "Save should overwrite existing file")]
    public void Test4()
    {
        // Arrange
        var initialGroups = _groupFaker.Generate(2).ToList();
        var newGroups = _groupFaker.Generate(3).ToList();

        // Act
        _dataService.Save(initialGroups);
        _dataService.Save(newGroups);
        var loadedGroups = _dataService.Load().ToList();

        // Assert
        Assert.Equal(newGroups.Count, loadedGroups.Count);
    }

    [Fact(DisplayName = "Save and Load should handle large datasets")]
    public void Test5()
    {
        // Arrange
        var largeGroups = _groupFaker.Generate(10000).ToList();

        // Act
        _dataService.Save(largeGroups);
        var loadedGroups = _dataService.Load().ToList();

        // Assert
        Assert.Equal(largeGroups.Count, loadedGroups.Count);
    }

    [Fact(DisplayName = "Save and Load should handle special characters in data correctly")]
    public void Test6()
    {
        // Arrange
        var specialGroup = new ServerGroup
        {
            Name = "Gr@up!@#",
            Description = "Th!s is @ t3st gr0up w!th $p3c!@l ch@r@ct3rs.",
            Servers =
            [
                new Server
                {
                    Name = "S3rv3r!@#",
                    Description = "T3st s3rv3r w!th sp3c!@l ch@rs.",
                    Host = "h0st!@#",
                    Username = "us3rn@m3!@#",
                    Password = "p@ssw0rd!@#",
                    Port = 12345,
                    GroupName = "Gr@up!@#"
                }
            ]
        };

        // Act
        _dataService.Save([specialGroup]);
        var loadedGroups = _dataService.Load().ToList();

        // Assert
        Assert.Single(loadedGroups);
        Assert.Equal(specialGroup.Name, loadedGroups[0].Name);
        Assert.Equal(specialGroup.Description, loadedGroups[0].Description);
        Assert.Equal(specialGroup.Servers.Count, loadedGroups[0].Servers.Count);
    }
}