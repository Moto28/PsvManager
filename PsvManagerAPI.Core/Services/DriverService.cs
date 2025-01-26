using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Infrastructure.Data.Interfaces;
using PsvManagerAPI.Core.Interfaces;
using PsvManagerAPI.Core.Models;

namespace PsvManagerAPI.Core.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<DriverService> _logger;

        public DriverService(ILogger<DriverService> logger, IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<Result<Driver>> AddDriver(Driver driver)
        {
            // Check if the driver already exists
            var existingDriver = await _driverRepository.GetByIdAsync(driver.Id);
            if (existingDriver != null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Driver already exists",
                    Detail = $"Driver with id {driver.Id} already exists",
                    Status = 409
                };

                _logger.LogError($"Driver with id {driver.Id} already exists");
                return Result<Driver>.Failure(problemDetails);
            }

            // Add the new driver
            var result = await _driverRepository.AddAsync(driver);
            if (result == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Error occurred while creating driver",
                    Detail = "Failed to add driver to the repository",
                    Status = 500
                };

                _logger.LogError("Error occurred while creating driver");
                return Result<Driver>.Failure(problemDetails);
            }
            return Result<Driver>.Success(driver);
        }

        public async Task<Result<bool>> DeleteDriver(Guid id)
        {
            var existingDriver = await _driverRepository.GetByIdAsync(id);
            if (existingDriver == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Driver not found",
                    Detail = $"Driver with id {id} not found",
                    Status = 404
                };

                _logger.LogError($"Driver with id {id} not found");
                return Result<bool>.Failure(problemDetails);
            }

            var result = await _driverRepository.DeleteAsync(id);
            if (result == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Error occurred while deleting driver",
                    Detail = "Failed to delete driver from the repository",
                    Status = 500
                };

                _logger.LogError("Error occurred while deleting driver");
                return Result<bool>.Failure(problemDetails);
            }
            return Result<bool>.Success(true);
        }

        public async Task<Result<IEnumerable<Driver>>> GetAllDrivers()
        {
            var drivers = await _driverRepository.GetAllAsync();
            if (drivers == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Error occurred while retrieving all drivers",
                    Detail = "Failed to retrieve drivers from the repository",
                    Status = 500
                };

                _logger.LogError("Error occurred while retrieving all drivers");
                return Result<IEnumerable<Driver>>.Failure(problemDetails);
            }
            return Result<IEnumerable<Driver>>.Success(drivers);
        }

        public async Task<Result<IEnumerable<Driver>>> GetAllDriversWithAddress()
        {
            var drivers = await _driverRepository.GetAllWithAddressAsync();
            if (drivers == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Error occurred while retrieving all drivers with address",
                    Detail = "Failed to retrieve drivers with address from the repository",
                    Status = 500
                };

                _logger.LogError("Error occurred while retrieving all drivers with address");
                return Result<IEnumerable<Driver>>.Failure(problemDetails);
            }
            return Result<IEnumerable<Driver>>.Success(drivers);
        }

        public async Task<Result<Driver>> GetDriverById(Guid id)
        {
            var driver = await _driverRepository.GetByIdAsync(id);
            if (driver == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Driver not found",
                    Detail = $"Driver with id {id} not found",
                    Status = 404
                };

                _logger.LogError($"Driver with id {id} not found");
                return Result<Driver>.Failure(problemDetails);
            }
            return Result<Driver>.Success(driver);
        }

        public async Task<Result<Driver>> UpdateDriver(Guid id, Driver driver)
        {
            var existingDriver = await _driverRepository.GetByIdAsync(id);
            if (existingDriver == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Driver not found",
                    Detail = $"Driver with id {id} not found",
                    Status = 404
                };

                _logger.LogError($"Driver with id {id} not found");
                return Result<Driver>.Failure(problemDetails);
            }

            existingDriver.Forename = driver.Forename;
            existingDriver.Surname = driver.Surname;
            existingDriver.LicenseNumber = driver.LicenseNumber;

            var result = await _driverRepository.UpdateAsync(existingDriver);
            if (result == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Error occurred while updating driver",
                    Detail = "Failed to update driver in the repository",
                    Status = 500
                };

                _logger.LogError($"Error occurred while updating driver with id: {id}");
                return Result<Driver>.Failure(problemDetails);
            }

            return Result<Driver>.Success(existingDriver);
        }
    }
}

