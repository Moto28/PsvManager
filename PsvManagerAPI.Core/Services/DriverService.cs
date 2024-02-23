using Microsoft.Extensions.Logging;
using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Infrastructure.Data.Interfaces;
using PsvManager.Infrastructure.Data.Repos;
using PsvManagerAPI.Core.Interfaces;

namespace PsvManagerAPI.Core.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(ILogger<DriverService> logger, IDriverRepository driverRepository) 
        {
            _driverRepository = driverRepository;
        }

        public Task<Driver> CreateDriver(Driver driver)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDriver(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Driver>> GetAllDrivers()
        {
            return await _driverRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Driver>> GetAllDriversWithAddress()
        {
            return await _driverRepository.GetAllWithAddressAsync();
        }

        public Task<Driver> GetDriverById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Driver> UpdateDriver(int id, Driver driver)
        {
            throw new NotImplementedException();
        }

        // Rest of the code...
    }
}