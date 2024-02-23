

using PsvManager.Infrastructure.Data.Entities;

namespace PsvManagerAPI.Core.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAllDrivers();
        Task<IEnumerable<Driver>> GetAllDriversWithAddress();
        Task<Driver> GetDriverById(int id);
        Task<Driver> CreateDriver(Driver driver);
        Task<Driver> UpdateDriver(int id, Driver driver);
        Task<bool> DeleteDriver(int id);
    }
}
