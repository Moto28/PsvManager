using Microsoft.EntityFrameworkCore;
using Moq;
using PsvManager.Infrastructure.Data.Contexts;
using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Infrastructure.Data.Repos;
using Xunit;

namespace PsvManager.Tests
{
    public class DriverRepositoryTests
    {
        //[Fact]
        //public void Add_ShouldAddEntityToContext()
        //{
        //    // Arrange
        //    var mockOptions = new DbContextOptions<PsvContext>();
        //    var mockContext = new Mock<PsvContext>(mockOptions);
        //    var repository = new DriverRepository(mockContext.Object);
        //    var driver = new Driver();

        //    // Act
        //    repository.Add(driver);

        //    // Asserts
        //    mockContext.Verify(c => c.Add(driver), Times.Once);
        //}

        //[Fact]
        //public void Delete_ShouldRemoveEntityFromContext()
        //{
        //    // Arrange
        //    var mockOptions = new DbContextOptions<PsvContext>();
        //    var mockContext = new Mock<PsvContext>(mockOptions);
        //    var repository = new DriverRepository(mockContext.Object);
        //    var driver = new Driver();

        //    // Act
        //    repository.Delete(driver);

        //    // Assert
        //    mockContext.Verify(c => c.Remove(driver), Times.Once);
        //}

        //[Fact]
        //public void GetById_ShouldReturnEntityFromContext()
        //{
        //    // Arrange
        //    var mockOptions = new DbContextOptions<PsvContext>();
        //    var mockContext = new Mock<PsvContext>(mockOptions);
        //    var repository = new DriverRepository(mockContext.Object);
        //    var driverId = Guid.NewGuid();
        //    var driver = new Driver { Id = driverId };
        //    mockContext.Setup(c => c.Find<Driver>(driverId)).Returns(driver);

        //    // Act
        //    var result = repository.GetById(driverId);

        //    // Assert
        //    Assert.Equal(driver, result);
        //}

        //[Fact]
        //public void Update_ShouldUpdateEntityInContext()
        //{
        //    // Arrange
        //    var mockOptions = new DbContextOptions<PsvContext>();
        //    var mockContext = new Mock<PsvContext>(mockOptions);
        //    var repository = new DriverRepository(mockContext.Object);
        //    var driver = new Driver();

        //    // Act
        //    repository.Update(driver);

        //    // Assert
        //    mockContext.Verify(c => c.Update(driver), Times.Once);
        //}

        //[Fact]
        //public void GetAll_ShouldReturnAllEntitiesFromContext()
        //{
        //    // Arrange
        //    var mockOptions = new DbContextOptions<PsvContext>();
        //    var mockContext = new Mock<PsvContext>(mockOptions);
        //    var repository = new DriverRepository(mockContext.Object);
        //    var drivers = new List<Driver> { new Driver(), new Driver(), new Driver() };
        //    var driversQueryable = drivers.AsQueryable();
        //    var mockSet = new Mock<DbSet<Driver>>();
        //    mockSet.As<IQueryable<Driver>>().Setup(m => m.Provider).Returns(driversQueryable.Provider);
        //    mockSet.As<IQueryable<Driver>>().Setup(m => m.Expression).Returns(driversQueryable.Expression);
        //    mockSet.As<IQueryable<Driver>>().Setup(m => m.ElementType).Returns(driversQueryable.ElementType);
        //    mockSet.As<IQueryable<Driver>>().Setup(m => m.GetEnumerator()).Returns(driversQueryable.GetEnumerator());
        //    mockContext.Setup(c => c.Set<Driver>()).Returns(mockSet.Object);

        //    // Act
        //    var result = repository.GetAll();

        //    // Assert
        //    Assert.Equal(drivers, result);
        //}
    }
}
