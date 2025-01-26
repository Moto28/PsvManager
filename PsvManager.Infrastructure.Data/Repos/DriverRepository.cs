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

        public async Task<Driver> AddAsync(Driver entity)
        {
            await _context.Drivers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Driver> DeleteAsync(Guid id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<IEnumerable<Driver>> GetAllWithAddressAsync()
        {
            return await _context.Drivers.Include(d => d.Address).ToListAsync(); ;
        }

        public async Task<Driver> GetByIdAsync(Guid id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public async Task<Driver> UpdateAsync(Driver entity)
        {
            _context.Drivers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
