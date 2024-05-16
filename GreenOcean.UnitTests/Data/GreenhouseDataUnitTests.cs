using FluentAssertions;
using GreenOcean.Data;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using GreenOcean.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GreenOcean.UnitTests.Data;

[TestClass]
public class GreenhouseDataUnitTests
{
    private readonly GreenhouseRepository _greenhouseRepository;
    private DbContextOptions<DataContext> _dbContextOptions;
    private Mock<IUserRepository> _mockUserRepository;
    private DataContext _dataContext;

    public GreenhouseDataUnitTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dataContext = new DataContext(_dbContextOptions);

        _mockUserRepository = new Mock<IUserRepository>();
        _greenhouseRepository = new GreenhouseRepository(_dataContext, _mockUserRepository.Object);
    }

    [TestMethod]
    public async Task GetGreenhousesUnitTest()
    {
        // Arrange
        (var userId, var username) = await AddUser();
        var greenhouseId = Guid.NewGuid();
        var greenhouse = new Greenhouse
        {
            Id = greenhouseId,
            Name = "Greenhouse 1",
            Street = "Street 1",
            Number = 1,
            City = "City 1",
            UserId = userId
        };

        await _dataContext.Greenhouses.AddAsync(greenhouse);
        await _dataContext.SaveChangesAsync();

        _mockUserRepository.Setup(u => u.GetUserByUsername(username)).ReturnsAsync(userId);

        // Act
        var resultGreenhouses = await _greenhouseRepository.GetGreenhouses(username);

        // Assert
        Assert.IsNotNull(resultGreenhouses);
        var resultGreenhouse = resultGreenhouses.FirstOrDefault(g => g.Id == greenhouseId);
        Assert.IsNotNull(resultGreenhouse);
    }

    [TestMethod]
    public async Task AddGreenhouseUnitTest()
    {
        // Arrange
        (var userId, var username) = await AddUser();
        var greenhouseId = Guid.NewGuid();
        var greenhouse = new Greenhouse
        {
            Id = greenhouseId,
            Name = "Greenhouse 1",
            Street = "Street 1",
            Number = 1,
            City = "City 1",
            UserId = userId
        };

        _mockUserRepository.Setup(r => r.GetUserByUsername(username)).ReturnsAsync(userId);

        // Act
        var response = await _greenhouseRepository.AddGreenhouse(username, greenhouse);

        // Assert
        Assert.IsNotNull(response);
        response.Should().Be(true);
    }    
    
    [TestMethod]
    public async Task EditGreenhouseUnitTest()
    {
        // Arrange
        (var userId, var username) = await AddUser();
        var greenhouseId = Guid.NewGuid();
        var greenhouseToEdit = new Greenhouse
        {
            Id = greenhouseId,
            Name = "Greenhouse 1",
            Street = "Street 1",
            Number = 1,
            City = "City 1",
            UserId = userId
        };

        await _dataContext.Greenhouses.AddAsync(greenhouseToEdit);
        await _dataContext.SaveChangesAsync();

        var greenhouse = new Greenhouse
        {
            Name = "Greenhouse 2",
            Street = "Street 2",
            Number = 1,
            City = "City 1",
            UserId = userId
        };

        // Act
        var response = await _greenhouseRepository.EditGreenhouse(greenhouseToEdit, greenhouse);

        // Assert
        Assert.IsNotNull(response);
        response.Should().Be(true);
    }    
    
    [TestMethod]
    public async Task DeleteGreenhouseUnitTest()
    {
        // Arrange
        (var userId, var username) = await AddUser();
        var greenhouseId = Guid.NewGuid();
        var greenhouse = new Greenhouse
        {
            Id = greenhouseId,
            Name = "Greenhouse 1",
            Street = "Street 1",
            Number = 1,
            City = "City 1",
            UserId = userId
        };

        await _dataContext.Greenhouses.AddAsync(greenhouse);
        await _dataContext.SaveChangesAsync();

        // Act
        var response = await _greenhouseRepository.DeleteGreenhouse(greenhouse);

        // Assert
        Assert.IsNotNull(response);
        response.Should().Be(true);
    }

    private async Task<(Guid userId, string username)> AddUser()
    {
        var userId = Guid.NewGuid();
        var username = "User";
        var user = new User
        {
            Id = userId,
            Username = username,
            Password = new byte[0],
            Salt = new byte[0],
            Email = "user1@example.com",
            Role = "User",
            FirstName = "First1",
            LastName = "Last1"
        };

        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();

        return (userId, username);
    }
}