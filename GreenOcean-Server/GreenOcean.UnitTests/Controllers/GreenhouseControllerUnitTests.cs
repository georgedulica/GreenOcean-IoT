using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GreenOcean.UnitTests.Controllers
{
    [TestClass]
    public class GreenhouseControllerUnitTests
    {
        private readonly GreenhouseController _greenhouseController;
        private readonly Mock<IGreenhouseService> _greenhouseService;

        public GreenhouseControllerUnitTests()
        {
            _greenhouseService = new Mock<IGreenhouseService>();
            _greenhouseController = new GreenhouseController(_greenhouseService.Object);
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

            var greenhouseDTO = new GreenhouseDTO
            {
                Id = id,
                Name = name,
                Street = street,
                City = city,
                Number = number
            };

            var greenhouseDTOs = new List<GreenhouseDTO>
            {
                greenhouseDTO
            };

            var username = "user";

            _greenhouseService.Setup(r => r.GetGreenhouses(It.IsAny<string>())).ReturnsAsync(greenhouseDTOs);

            // Act
            var response = await _greenhouseController.GetGreenhouses(username);

            // Assert
            Assert.IsNotNull(response);
            var result = response.Result as OkObjectResult;
            Assert.IsNotNull(result);
            var value = result.Value;
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public async Task AddGreenhouseTest()
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
            _greenhouseService.Setup(r => r.AddGreenhouse(username, greenhouseDTO))
                              .ReturnsAsync(true);

            // Act
            var response = await _greenhouseController.AddGreenhouse(username, greenhouseDTO);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public async Task EditGreenhouseTest()
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

            var greenhouseId = Guid.NewGuid();

            _greenhouseService.Setup(r => r.EditGreenhouse(greenhouseId, greenhouseDTO))
                              .ReturnsAsync(true);

            // Act
            var response = await _greenhouseController.EditGreenhouse(greenhouseId, greenhouseDTO);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteGreenhouseTest()
        {
            var greenhouseId = Guid.NewGuid();
            _greenhouseService.Setup(r => r.DeleteGreenhouse(greenhouseId))
                              .ReturnsAsync(true);

            // Act
            var response = await _greenhouseController.DeleteGreenhouse(greenhouseId);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }
    }
}
