using InvoiceApp.WebApi.Data;
using Microsoft.EntityFrameworkCore;
namespace JLoka.InvoiceApp.UnitTests
{
    internal class TestDatabaseFixture
    {
        private const string ConnectionString = @"Server=DESKTOP-3UNEJDA\\SQLEXPRESS;Database=JLokaDB_02;
        Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

        public InvoiceDbContext CreateDbContext() => new(new DbContextOptionsBuilder<InvoiceDbContext>().UseSqlServer(ConnectionString).Options, null);
    }
}
