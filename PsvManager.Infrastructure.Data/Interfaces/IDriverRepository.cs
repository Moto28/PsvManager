using Microsoft.EntityFrameworkCore;
using PsvManager.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsvManager.Infrastructure.Data.Interfaces
{
    public interface IDriverRepository
    {
        public Task<Driver> AddAsync(Driver entity);
        public Task<Driver> DeleteAsync(Guid entity);   
        public Task<Driver> UpdateAsync(Driver entity);
        public Task<Driver> GetByIdAsync(Guid id);
        public Task<IEnumerable<Driver>> GetAllAsync();
        public Task<IEnumerable<Driver>> GetAllWithAddressAsync();

    }
}
