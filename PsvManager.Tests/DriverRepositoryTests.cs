using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Tests.Fixtures;

namespace PsvManager.Tests
{
    [Collection("DriverRepositoryTests")]
    public class DriverRepositoryTests
    {
        private readonly DriverRepositoryFixture _fixture;

        public DriverRepositoryTests(DriverRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AddDriverAsync_WhereNoDriverExistingFound_AddsDriverToDatabase()
        {
            await _fixture.ResetDatabaseAsync();
            var address = CreateAddress();
            var driver = CreateDriverWithAddress(address);

            var result = await _fixture.DriverRepository.AddAsync(driver);

            Assert.Equal("John", result.Forename);
            Assert.Equal("Doe", result.Surname);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveDriver()
        {
            await _fixture.ResetDatabaseAsync();
            var address = CreateAddress();
            var driver = CreateDriverWithAddress(address);
            _fixture.Context.Drivers.Add(driver);
            _fixture.Context.SaveChanges();

            var result = await _fixture.DriverRepository.DeleteAsync(driver.Id);

            Assert.Equal(0, _fixture.Context.Drivers.Count());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllDrivers()
        {
            await _fixture.ResetDatabaseAsync();
            var driver1 = CreateDriverWithoutAddress();
            var driver2 = CreateDriverWithoutAddress();

            _fixture.Context.Drivers.Add(driver1);
            _fixture.Context.Drivers.Add(driver2);
            _fixture.Context.SaveChanges();

            var drivers = await _fixture.DriverRepository.GetAllAsync();

            Assert.Equal(2, drivers.Count());
        }

        [Fact]
        public async Task GetAllWithAddressAsync_ShouldReturnAllDriversWithAddress()
        {
            await _fixture.ResetDatabaseAsync();
            var address1 = CreateAddress();
            var driver1 = CreateDriverWithAddress(address1);
            var address2 = CreateAddress();
            var driver2 = CreateDriverWithAddress(address2);

            _fixture.Context.Drivers.Add(driver1);
            _fixture.Context.Drivers.Add(driver2);
            _fixture.Context.SaveChanges();

            var drivers = await _fixture.DriverRepository.GetAllWithAddressAsync();

            Assert.Equal(2, drivers.Count());
            Assert.All(drivers, d => Assert.NotNull(d.Address));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDriver()
        {
            await _fixture.ResetDatabaseAsync();
            var address = CreateAddress();
            var driver = CreateDriverWithAddress(address);

            _fixture.Context.Drivers.Add(driver);
            _fixture.Context.SaveChanges();

            var result = await _fixture.DriverRepository.GetByIdAsync(driver.Id);

            Assert.NotNull(result);
            Assert.Equal("John", result.Forename);
            Assert.Equal("Doe", result.Surname);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateDriver()
        {
            await _fixture.ResetDatabaseAsync();
            var address = CreateAddress();
            var driver = CreateDriverWithAddress(address);

            _fixture.Context.Drivers.Add(driver);
            _fixture.Context.SaveChanges();

            driver.Forename = "John";
            driver.Surname = "Smith";
            await _fixture.DriverRepository.UpdateAsync(driver);

            var updatedDriver = _fixture.Context.Drivers.Single();

            Assert.Equal("John", updatedDriver.Forename);
            Assert.Equal("Smith", updatedDriver.Surname);
        }

        [Fact]
        public async Task GetAddressByIdAsync_ShouldReturnAddress()
        {
            await _fixture.ResetDatabaseAsync();
            var address = CreateAddress();
            _fixture.Context.Addresses.Add(address);
            _fixture.Context.SaveChanges();

            var result = await _fixture.DriverRepository.GetAddressByIdAsync(address.Id);

            Assert.NotNull(result);
            Assert.Equal("123", result.HouseNumber);
            Assert.Equal("Main St", result.StreetName);
        }

        [Fact]
        public async Task AddAddressAsync_ShouldAddAddressToDatabase()
        {
            await _fixture.ResetDatabaseAsync();
            var address = CreateAddress();

            var result = await _fixture.DriverRepository.AddAddressAsync(address);

            Assert.NotNull(result);
            Assert.Equal("123", result.HouseNumber);
            Assert.Equal("Main St", result.StreetName);
            Assert.Equal(1, _fixture.Context.Addresses.Count());
        }

        private Address CreateAddress()
        {
            return new Address
            {
                Id = Guid.NewGuid(),
                HouseNumber = "123",
                StreetName = "Main St",
                TownOrCity = "Livingston",
                County = "West Lothian",
                Postcode = "12345"
            };
        }

        private Driver CreateDriverWithAddress(Address address)
        {
            return new Driver
            {
                Id = Guid.NewGuid(),
                Forename = "John",
                Surname = "Doe",
                LicenseNumber = "Test1234",
                Address = address
            };
        }

        private Driver CreateDriverWithoutAddress()
        {
            return new Driver
            {
                Id = Guid.NewGuid(),
                Forename = "John",
                Surname = "Doe",
                LicenseNumber = "Test1234"
            };
        }
    }
}