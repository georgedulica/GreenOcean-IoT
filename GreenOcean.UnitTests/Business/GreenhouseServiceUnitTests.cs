using AutoMapper;
using FluentAssertions;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Services;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GreenOcean.UnitTests.Business;

[TestClass]
public class GreenhouseServiceUnitTests
{
    private readonly Mock<IGreenhouseRepository> _greenhouseRepository;
    private readonly GreenhouseService _greenhouseService;
    private readonly IMapper _mapper;

    public GreenhouseServiceUnitTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Greenhouse, GreenhouseDTO>();
            cfg.CreateMap<GreenhouseDTO, Greenhouse>();
        });

        _mapper = config.CreateMapper();
        _greenhouseRepository = new Mock<IGreenhouseRepository>();
        _greenhouseService = new GreenhouseService(_greenhouseRepository.Object, _mapper);
    }

    [TestMethod]
    public async Task GetGreenhousesTest()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "sera";
        var street = "street";
        var number = 12;
        var city = "cluj-napoca";

        var greenhouse = new Greenhouse
        {
            Id = id,
            Name = name,
            Street = street,
            City = city,
            Number = number
        };

        var greenhouses = new List<Greenhouse>
        {
            greenhouse
        };

        var username = "user";
        _greenhouseRepository.Setup(r => r.GetGreenhouses(username))
                             .ReturnsAsync(greenhouses);

        // Act
        var response = await _greenhouseService.GetGreenhouses(username);

        // Assert
        Assert.IsNotNull(response);
        var resultGreenhouse = response.FirstOrDefault(r => r.Id == id);

        Assert.IsNotNull(resultGreenhouse);
        resultGreenhouse.Should().NotBeNull();
        resultGreenhouse.Id.Should().Be(id);
        resultGreenhouse.Name.Should().Be(name);
        resultGreenhouse.Street.Should().Be(street);
        resultGreenhouse.City.Should().Be(city);
        resultGreenhouse.Number.Should().Be(number);
    }

    [TestMethod]
    public async Task AddGreenhousesTest()
    {
        // Arrange
        var name = "sera";
        var street = "street";
        var number = 12;
        var city = "cluj-napoca";

        var greenhouseDTO = new GreenhouseDTO
        {
            Name = name,
            Street = street,
            City = city,
            Number = number
        };

        var username = "user";
        _greenhouseRepository.Setup(r => r.AddGreenhouse(username, It.IsAny<Greenhouse>()))
                     .ReturnsAsync(true);
        // Act
        var response = await _greenhouseService.AddGreenhouse(username, greenhouseDTO);

        // Assert
        Assert.IsNotNull(response);
        Assert.IsTrue(response);
    }

    [TestMethod]
    public async Task EditGreenhousesTest()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "sera";
        var street = "street";
        var number = 12;
        var city = "cluj-napoca";

        var greenhouseDTO = new GreenhouseDTO
        {
            Name = name,
            Street = street,
            City = city,
            Number = number
        };

        var greenhouse = new Greenhouse
        {
            Id = id,
            Name = name,
            Street = street,
            City = city,
            Number = number
        };

        _greenhouseRepository.Setup(r => r.GetGreenhouse(id)).ReturnsAsync(greenhouse);
        _greenhouseRepository.Setup(r => r.EditGreenhouse(It.IsAny<Greenhouse>(), 
            It.IsAny<Greenhouse>())).ReturnsAsync(true);

        // Act
        var response = await _greenhouseService.EditGreenhouse(id, greenhouseDTO);

        // Assert
        Assert.IsNotNull(response);
        Assert.IsTrue(response);
    }


    [TestMethod]
    public async Task DeleteGreenhousesTest()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "sera";
        var street = "street";
        var number = 12;
        var city = "cluj-napoca";

        var greenhouse = new Greenhouse
        {
            Id = id,
            Name = name,
            Street = street,
            City = city,
            Number = number
        };

        _greenhouseRepository.Setup(r => r.GetGreenhouse(id)).ReturnsAsync(greenhouse);
        _greenhouseRepository.Setup(r => r.DeleteGreenhouse(It.IsAny<Greenhouse>()))
                             .ReturnsAsync(true);

        // Act
        var response = await _greenhouseService.DeleteGreenhouse(id);

        // Assert
        Assert.IsNotNull(response);
        Assert.IsTrue(response);
    }
}