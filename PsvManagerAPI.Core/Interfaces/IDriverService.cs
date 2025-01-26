using PsvManager.Infrastructure.Data.Entities;
using PsvManagerAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsvManagerAPI.Core.Interfaces
{
    public interface IDriverService
    {
        Task<Result<IEnumerable<Driver>>> GetAllDrivers();
        Task<Result<IEnumerable<Driver>>> GetAllDriversWithAddress();
        Task<Result<Driver>> GetDriverById(Guid id);
        Task<Result<Driver>> AddDriver(Driver driver);
        Task<Result<Driver>> UpdateDriver(Guid id, Driver driver);
        Task<Result<bool>> DeleteDriver(Guid id);
    }
}
