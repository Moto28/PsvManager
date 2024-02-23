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
        public Task AddAsync(Driver entity);
        public Task Delete(Driver entity);   
        public Task Update(Driver entity);
        public Task<IEnumerable<Driver>> GetAllAsync();
        public Task<IEnumerable<Driver>> GetAllWithAddressAsync();

    }
}
