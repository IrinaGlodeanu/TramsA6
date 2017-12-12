using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistence.PersistenceFolder;

namespace IntegrationTests
{
    public abstract class BaseIntegrationTest : IDisposable
    {
        private IConfiguration _configuration;

        protected virtual bool UseSqlServer => false;// bool.Parse(_configuration["UseSqlServer"]);

        [TestInitialize]
        public virtual void TestInitialize()
        {

          //  InitializeConfiguration();
            DestroyDatabase();
            CreateDatabase();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            DestroyDatabase();
        }
   
        public void RunOnDatabase(Action<DatabaseContext> databaseAction)
        {
            if (UseSqlServer)
            {
                RunOnSqlServer(databaseAction);
            }
            else
            {
                RunOnMemory(databaseAction);
            }
        }

        private void RunOnSqlServer(Action<DatabaseContext> databaseAction)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer("Data Source=users.db")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                databaseAction(context);
            }
        }

        private void RunOnMemory(Action<DatabaseContext> databaseAction)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("Users")
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

        public void Dispose()
        {
            DestroyDatabase();
        }
    }

}
