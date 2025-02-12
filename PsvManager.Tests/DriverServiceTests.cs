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
        public async Task AddDriverAsync_ShouldReturnSuccess_WhenDriverIsAddedSuccessfully()
        {
            // Arrange
            var driver = CreateDriver();
            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driver.Id))
                .ReturnsAsync((Driver?)null);
            _mockDriverRepository.Setup(repo => repo.AddAsync(driver))
                .ReturnsAsync(driver);

            // Act
            var result = await _driverService.AddDriverAsync(driver);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(driver, result.Value);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driver.Id), Times.Once);
            _mockDriverRepository.Verify(repo => repo.AddAsync(driver), Times.Once);
            _logger.Verify(logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Driver with id {driver.Id} added successfully")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task AddDriverAsync_ShouldReturnFailure_WhenDriverAlreadyExists()
        {
            // Arrange
            var driver = CreateDriver();
            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driver.Id))
                .ReturnsAsync(driver);

            // Act
            var result = await _driverService.AddDriverAsync(driver);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(409, result.ProblemDetails?.Status);
            Assert.Equal($"Driver with id {driver.Id} already exists", result.ProblemDetails?.Detail);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driver.Id), Times.Once);
            _mockDriverRepository.Verify(repo => repo.AddAsync(It.IsAny<Driver>()), Times.Never);
            _logger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Driver with id {driver.Id} already exists")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task AddDriverAsync_ShouldReturnFailure_WhenAddDriverFails()
        {
            // Arrange
            var driver = CreateDriver();
            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driver.Id))
                .ReturnsAsync((Driver?)null);
            _mockDriverRepository.Setup(repo => repo.AddAsync(driver))
                .ReturnsAsync((Driver?)null);

            // Act
            var result = await _driverService.AddDriverAsync(driver);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(500, result.ProblemDetails?.Status);
            Assert.Equal("Failed to add driver to the repository", result.ProblemDetails?.Detail);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driver.Id), Times.Once);
            _mockDriverRepository.Verify(repo => repo.AddAsync(driver), Times.Once);
            _logger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error occurred while creating driver")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDriverAsync_ShouldRemoveDriver()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var driver = CreateDriver();
            driver.Id = driverId;

            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driverId))
                .ReturnsAsync(driver);
            _mockDriverRepository.Setup(repo => repo.DeleteAsync(driver))
                .ReturnsAsync(driverId);

            // Act
            var result = await _driverService.DeleteDriverAsync(driverId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(driverId, result.Value);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driverId), Times.Once);
            _mockDriverRepository.Verify(repo => repo.DeleteAsync(driver), Times.Once);
            _logger.Verify(logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Driver with id {driverId} deleted")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDriverAsync_ShouldReturnFailure_WhenDriverDoesNotExist()
        {
            // Arrange
            var driverId = Guid.NewGuid();

            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driverId))
                .ReturnsAsync((Driver?)null);

            // Act
            var result = await _driverService.DeleteDriverAsync(driverId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.ProblemDetails?.Status);
            Assert.Equal($"Driver with id {driverId} not found", result.ProblemDetails?.Detail);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driverId), Times.Once);
            _mockDriverRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Driver>()), Times.Never);
            _logger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Driver with id {driverId} not found")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task GetAllDriversAsync_ShouldReturnAllDrivers()
        {
            // Arrange
            var drivers = new List<Driver> { CreateDriver(), CreateDriver() };

            _mockDriverRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(drivers);

            // Act
            var result = await _driverService.GetAllDriversAsync();

            // Assert
            Assert.Equal(drivers, result.Value);
            _mockDriverRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetDriverByIdAsync_ShouldReturnDriver()
        {
            // Arrange
            var driver = CreateDriver();

            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driver.Id))
                .ReturnsAsync(driver);

            // Act
            var result = await _driverService.GetDriverByIdAsync(driver.Id);

            // Assert
            Assert.Equal(driver, result.Value);
            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driver.Id), Times.Once);
        }

        [Fact]
        public async Task GetDriverByIdAsync_ShouldReturnFailure_WhenDriverNotFound()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driverId))
                .ReturnsAsync((Driver)null);

            // Act
            var result = await _driverService.GetDriverByIdAsync(driverId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.ProblemDetails.Status);
            Assert.Equal($"Driver with id {driverId} not found", result.ProblemDetails.Detail);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driverId), Times.Once);
            _logger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Driver with id {driverId} not found")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDriverAsync_ShouldUpdateDriverProperties_WhenDriverExists()
        {
            // Arrange
            var driver = CreateDriver();
            var updatedDriver = CreateDriver();
            updatedDriver.Id = driver.Id;
            updatedDriver.Forename = "Jane";

            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driver.Id))
                .ReturnsAsync(driver);
            _mockDriverRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Driver>()))
                .ReturnsAsync(updatedDriver);

            // Act
            var result = await _driverService.UpdateDriverAsync(updatedDriver);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(updatedDriver.Id, result.Value.Id);
            Assert.Equal(updatedDriver.Forename, result.Value.Forename);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driver.Id), Times.Once);
            _mockDriverRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Driver>()), Times.Once);
            _logger.Verify(logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Driver with id {updatedDriver.Id} successfully updated.")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDriverAsync_ShouldReturnFailure_WhenUpdateDriverFails()
        {
            // Arrange
            var driver = CreateDriver();
            var updatedDriver = CreateDriver();
            updatedDriver.Id = driver.Id;
            updatedDriver.Forename = "Jane";

            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driver.Id))
                .ReturnsAsync(driver);
            _mockDriverRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Driver>()))
                .ReturnsAsync((Driver?)null);

            // Act
            var result = await _driverService.UpdateDriverAsync(updatedDriver);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(500, result.ProblemDetails?.Status);
            Assert.Equal("Failed to update driver in the repository", result.ProblemDetails?.Detail);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driver.Id), Times.Once);
            _mockDriverRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Driver>()), Times.Once);
            _logger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error occurred while updating driver")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDriverAsync_ShouldReturnFailure_WhenDriverDoesNotExist()
        {
            // Arrange
            var updatedDriver = CreateDriver();
            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(updatedDriver.Id))
                .ReturnsAsync((Driver?)null);

            // Act
            var result = await _driverService.UpdateDriverAsync(updatedDriver);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.ProblemDetails?.Status);
            Assert.Equal($"Driver with id {updatedDriver.Id} not found", result.ProblemDetails?.Detail);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(updatedDriver.Id), Times.Once);
            _mockDriverRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Driver>()), Times.Never);
            _logger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Driver with id {updatedDriver.Id} not found")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task GetAllDriversWithAddressAsync_ShouldReturnAllDriversWithAddress()
        {
            // Arrange
            var drivers = new List<Driver> { CreateDriver(), CreateDriver() };

            _mockDriverRepository.Setup(repo => repo.GetAllWithAddressAsync())
                .ReturnsAsync(drivers);

            // Act
            var result = await _driverService.GetAllDriversWithAddressAsync();

            // Assert
            Assert.Equal(drivers, result.Value);
            _mockDriverRepository.Verify(repo => repo.GetAllWithAddressAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllDriversWithAddressAsync_ShouldReturnFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            _mockDriverRepository.Setup(repo => repo.GetAllWithAddressAsync())
                .ReturnsAsync((IEnumerable<Driver>?)null);

            // Act
            var result = await _driverService.GetAllDriversWithAddressAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(500, result.ProblemDetails?.Status);
            Assert.Equal("Failed to retrieve drivers with address from the repository", result.ProblemDetails?.Detail);

            _mockDriverRepository.Verify(repo => repo.GetAllWithAddressAsync(), Times.Once);
            _logger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error occurred while retrieving all drivers with address")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task GetAllDriversAsync_ShouldReturnFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            _mockDriverRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync((IEnumerable<Driver>?)null);

            // Act
            var result = await _driverService.GetAllDriversAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(500, result.ProblemDetails?.Status);
            Assert.Equal("Failed to retrieve drivers from the repository", result.ProblemDetails?.Detail);

            _mockDriverRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            _logger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error occurred while retrieving all drivers")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDriverAsync_ShouldReturnFailure_WhenDeleteDriverFails()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var driver = CreateDriver();
            driver.Id = driverId;

            _mockDriverRepository.Setup(repo => repo.GetByIdAsync(driverId))
                .ReturnsAsync(driver);

            _mockDriverRepository.Setup(repo => repo.DeleteAsync(driver))
                .ReturnsAsync(Guid.Empty); // Simulate failure

            // Act
            var result = await _driverService.DeleteDriverAsync(driverId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(500, result.ProblemDetails?.Status);
            Assert.Equal("Failed to delete driver from the repository", result.ProblemDetails?.Detail);

            _mockDriverRepository.Verify(repo => repo.GetByIdAsync(driverId), Times.Once);
            _mockDriverRepository.Verify(repo => repo.DeleteAsync(driver), Times.Once);
            _logger.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error occurred while deleting driver")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once);
        }

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
