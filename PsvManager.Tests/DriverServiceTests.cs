using Microsoft.Extensions.Logging;
using Moq;
using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Infrastructure.Data.Interfaces;
using PsvManagerAPI.Core.Interfaces;
using PsvManagerAPI.Core.Services;

namespace PsvManager.Tests
{
    public class DriverServiceTests
    {
        private readonly Mock<IDriverRepository> _mockDriverRepository;
        private readonly Mock<ILogger<DriverService>> _logger;
        private readonly IDriverService _driverService;

        public DriverServiceTests()
        {
            _mockDriverRepository = new Mock<IDriverRepository>();
            _logger = new Mock<ILogger<DriverService>>();
            _driverService = new DriverService(_logger.Object, _mockDriverRepository.Object);
        }

        [Fact]
        public async Task AddDriverAsync_ShouldReturnFailure_WhenDriverAlreadyExists()
        {
            // Arrange
            var driver = CreateDriver();
            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driver.Id))
                .ReturnsAsync(driver);

            // Act
            var result = await _driverService.AddDriver(driver);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(409, result.ProblemDetails.Status);
            Assert.Equal($"Driver with id {driver.Id} already exists", result.ProblemDetails.Detail);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driver.Id), Times.Once);
            _mockDriverRepository.Verify(repo => repo.AddAsync(It.IsAny<Driver>()), Times.Never);
        }

        [Fact]
        public async Task AddDriverAsync_ShouldReturnSuccess_WhenDriverDoesNotExist()
        {
            // Arrange
            var driver = CreateDriver();
            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driver.Id))
                .ReturnsAsync((Driver)null);
            _mockDriverRepository.Setup(repo => repo.AddAsync(driver))
                .ReturnsAsync(driver);

            // Act
            var result = await _driverService.AddDriver(driver);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(driver.Id, result.Value.Id);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driver.Id), Times.Once);
            _mockDriverRepository.Verify(repo => repo.AddAsync(driver), Times.Once);
        }

        //[Fact]
        //public async Task DeleteDriverAsync_ShouldRemoveDriver()
        //{
        //    // Arrange
        //    var driverId = Guid.NewGuid();

        //    _mockDriverRepository.Setup(repo => repo.DeleteAsync(driverId))
        //        .Returns(Task.CompletedTask);

        //    // Act
        //    await _driverService.DeleteDriverAsync(driverId);

        //    // Assert
        //    _mockDriverRepository.Verify(repo => repo.DeleteAsync(driverId), Times.Once);
        //}

        //[Fact]
        //public async Task GetAllDriversAsync_ShouldReturnAllDrivers()
        //{
        //    // Arrange
        //    var drivers = new List<Driver> { CreateDriver(), CreateDriver() };

        //    _mockDriverRepository.Setup(repo => repo.GetAllAsync())
        //        .ReturnsAsync(drivers);

        //    // Act
        //    var result = await _driverService.GetAllDriversAsync();

        //    // Assert
        //    Assert.Equal(drivers, result);
        //    _mockDriverRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        //}

        //[Fact]
        //public async Task GetDriverByIdAsync_ShouldReturnDriver()
        //{
        //    // Arrange
        //    var driver = CreateDriver();

        //    _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driver.Id))
        //        .ReturnsAsync(driver);

        //    // Act
        //    var result = await _driverService.GetDriverByIdAsync(driver.Id);

        //    // Assert
        //    Assert.Equal(driver, result);
        //    _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driver.Id), Times.Once);
        //}

        //[Fact]
        //public async Task UpdateDriverAsync_ShouldUpdateDriver()
        //{
        //    // Arrange
        //    var driver = CreateDriver();

        //    _mockDriverRepository.Setup(repo => repo.UpdateAsync(driver))
        //        .Returns(Task.CompletedTask);

        //    // Act
        //    await _driverService.UpdateDriverAsync(driver);

        //    // Assert
        //    _mockDriverRepository.Verify(repo => repo.UpdateAsync(driver), Times.Once);
        //}

        private Driver CreateDriver()
        {
            return new Driver
            {
                Id = Guid.NewGuid(),
                Forename = "John",
                Surname = "Doe",
                LicenseNumber = "Test1234",
                Address = new Address
                {
                    Id = Guid.NewGuid(),
                    HouseNumber = "123",
                    StreetName = "Main St",
                    TownOrCity = "Livingston",
                    County = "West Lothian",
                    Postcode = "12345"
                }
            };
        }
    }
}
