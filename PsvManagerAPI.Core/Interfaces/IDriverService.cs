using PsvManager.Infrastructure.Data.Entities;
using PsvManagerAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsvManagerAPI.Core.Interfaces
{
    public interface IDriverService
    {
        Task<Result<IEnumerable<Driver>>> GetAllDriversAsync();
        Task<Result<IEnumerable<Driver>>> GetAllDriversWithAddressAsync();
        Task<Result<Driver>> GetDriverByIdAsync(Guid id);
        Task<Result<Driver>> AddDriverAsync(Driver driver);
        Task<Result<Driver>> UpdateDriverAsync(Driver driver);
        Task<Result<Guid>> DeleteDriverAsync(Guid id);     
    }
}
