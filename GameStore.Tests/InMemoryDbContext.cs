using GameStore.Areas.Manager.Controllers;
using GameStore.Data.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Tests
{
    public class InMemoryDbContext
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<GameStoreDbContext> dbContextOptions;

        public InMemoryDbContext()
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<GameStoreDbContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new GameStoreDbContext(dbContextOptions);

            context.Database.EnsureCreated();
        }

        public GameStoreDbContext CreateContext() => new GameStoreDbContext(dbContextOptions);

        public void Dispose() => connection.Dispose();
    }
}
