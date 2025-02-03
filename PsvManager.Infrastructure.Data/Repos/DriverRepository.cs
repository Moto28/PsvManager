using Microsoft.EntityFrameworkCore;
using PsvManager.Infrastructure.Data.Contexts;
using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Infrastructure.Data.Interfaces;

namespace PsvManager.Infrastructure.Data.Repos
{
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository
    {
        private readonly PsvContext _context;

        public DriverRepository(PsvContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Driver>> GetAllWithAddressAsync()
        {
            return await _context.Drivers.Include(d => d.Address).ToListAsync();
        }

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<Address> AddAddressAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }
    }
}
