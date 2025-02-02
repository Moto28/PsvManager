using Xunit;
using PsvManager.Tests.Fixtures;

namespace PsvManager.Tests.Collections
{
    [CollectionDefinition("DriverRepositoryTests")]
    public class DriverRepositoryTestsCollection : ICollectionFixture<DriverRepositoryFixture> { }
}
