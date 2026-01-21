using InvoiceApp.WebApi.Data;
using Microsoft.EntityFrameworkCore;
namespace JLoka.InvoiceApp.UnitTests
{
    internal class TestDatabaseFixture
    {
        private const string ConnectionString = @"Server=DESKTOP-3UNEJDA\\SQLEXPRESS;Database=JLokaDB_02;
        Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

        private static readonly object Lock = new();
        private static bool _databaseInitialized;

        public TestDatabaseFixture()
        {
            // This code comes from Microsoft Docs: https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/Testing/TestingWithTheDatabase/TestDatabaseFixture.cs
            lock (Lock)
            {
                if (!_databaseInitialized!)
                {
                    InitializeDatabase();
                    _databaseInitialized = true;
                }
            }
        }


        public InvoiceDbContext CreateDbContext() => new(new DbContextOptionsBuilder<InvoiceDbContext>().UseSqlServer(ConnectionString).Options, null);

        public void InitializeDatabase()
        {
            using var context = CreateDbContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
