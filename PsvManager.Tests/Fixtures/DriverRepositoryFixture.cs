using Microsoft.EntityFrameworkCore;
using PsvManager.Infrastructure.Data.Contexts;
using PsvManager.Infrastructure.Data.Interfaces;
using PsvManager.Infrastructure.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsvManager.Tests.Fixtures
{
    public class DriverRepositoryFixture : IAsyncDisposable
    {
        public IDriverRepository DriverRepository { get; private set; }
        public PsvContext Context { get; private set; }

        public DriverRepositoryFixture()
        {
            var options = new DbContextOptionsBuilder<PsvContext>()
            .UseInMemoryDatabase(databaseName: "PsvTestDatabase")
            .Options;

            Context = new PsvContext(options);
            DriverRepository = new DriverRepository(Context);
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
        }

        public async Task ResetDatabaseAsync()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();            
        }
    }
}