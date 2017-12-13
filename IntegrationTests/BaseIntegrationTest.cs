using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.PersistenceFolder;

namespace IntegrationTests
{
    public abstract class BaseIntegrationTest : IDisposable
    {
        private IConfiguration _configuration;


        public BaseIntegrationTest()
        {
            InitializeConfiguration();
            DestroyDatabase();
            CreateDatabase();
        }

        protected virtual bool UseSqlServer => bool.Parse(_configuration["UseSqlServer"]);

        public void Dispose()
        {
            DestroyDatabase();
        }

        public void RunOnDatabase(Action<DatabaseContext> databaseAction)
        {
            if (UseSqlServer)
                RunOnSqlServer(databaseAction);
            else
                RunOnMemory(databaseAction);
        }

        private void RunOnSqlServer(Action<DatabaseContext> databaseAction)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(_configuration.GetConnectionString("UsersConnectionString"))
                .Options;

            using (var context = new DatabaseContext(options))
            {
                databaseAction(context);
            }
        }

        private void RunOnMemory(Action<DatabaseContext> databaseAction)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("UsersConnectionString")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                databaseAction(context);
            }
        }

        private void InitializeConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
        }

        private void CreateDatabase()
        {
            RunOnDatabase(context => context.Database.EnsureCreated());
        }

        private void DestroyDatabase()
        {
            RunOnDatabase(context => context.Database.EnsureDeleted());
        }
    }
}