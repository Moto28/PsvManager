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
        public Task<IEnumerable<Driver>> GetAllWithAddressAsync();
        public Task<Address> GetAddressByIdAsync(Guid id);
        public Task<Address> AddAddressAsync(Address address);

    }
}
