using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using PsvManager.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsvManager.Infrastructure.Data.Contexts
{
    public class PsvContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        private readonly DbContextOptions<PsvContext> _options;
        public PsvContext(DbContextOptions<PsvContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
                // Add your entity configurations here
                var addressId1 = Guid.NewGuid();
                var addressId2 = Guid.NewGuid();

                var address1 = new Address { Id = addressId1, HouseNumber = "123", StreetName = "Main St", TownOrCity = "New York", County = null, Postcode = "12345" };
                var address2 = new Address { Id = addressId2, HouseNumber = "456", StreetName = "Elm St", TownOrCity = "Los Angeles", County = null, Postcode = "67890" };

                var driverId1 = Guid.NewGuid();
                var driverId2 = Guid.NewGuid();

                var driver1 = new Driver { Id = driverId1, Forename = "John", Surname = "Bon Jovi", LicenseNumber = "Test1234", AddressId = addressId1 };
                var driver2 = new Driver { Id = driverId2, Forename = "Craig", Surname = "Cheney", LicenseNumber = "Test4567", AddressId = addressId2 };

                var vehicle1 = new Vehicle { Id = Guid.NewGuid(), Make = "Toyota", Model = "Camry", Registration = "ABC123", MaxPassengers = 5 };
                var vehicle2 = new Vehicle { Id = Guid.NewGuid(), Make = "Honda", Model = "Accord", Registration = "DEF456", MaxPassengers = 4 };

                modelBuilder.Entity<Address>().HasData(address1, address2);
                modelBuilder.Entity<Driver>().HasData(driver1, driver2);
                modelBuilder.Entity<Vehicle>().HasData(vehicle1, vehicle2);
         
                base.OnModelCreating(modelBuilder);
        }
    }
}
