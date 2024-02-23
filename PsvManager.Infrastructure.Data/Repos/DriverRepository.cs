using Microsoft.EntityFrameworkCore;
using PsvManager.Infrastructure.Data.Contexts;
using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Infrastructure.Data.Interfaces;

namespace PsvManager.Infrastructure.Data.Repos
{
    public class DriverRepository : IDriverRepository
    {
        private readonly PsvContext _context;

        public DriverRepository(PsvContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<IEnumerable<Driver>> GetAllWithAddressAsync()
        {
            return await _context.Drivers.Include(d => d.Address).ToListAsync(); ;
        }

        public Task AddAsync(Driver entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Driver entity)
        {
            throw new NotImplementedException();
        }

        public Task<Driver> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Driver entity)
        {
            throw new NotImplementedException();
        }       
    }
}
