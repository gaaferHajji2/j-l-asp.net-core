using JLokaTestEFCore.enums;
using JLokaTestEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestEFCore.Data
{
    public class InvoiceDbContext(DbContextOptions<InvoiceDbContext> options): DbContext(options)
    {
        public DbSet<Invoice> Invoices => Set<Invoice>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().HasData(new Invoice
            {
                Id = new Guid("3212976a-1f5a-462b-b989-17a80f9f6e79"),
                InvoiceNumber = "JLoka-01",
                ContactName = "JLoka",
                Description = "Simple Description",
                Amount = 500000,
                InvoiceDate = new DateTimeOffset(2025, 1, 1, 1, 0, 0, TimeSpan.Zero),
                DueDate = new DateTimeOffset(2025, 1, 1, 1, 0, 0, TimeSpan.Zero),
                Status = enums.InvoiceStatus.AwaitPayment,
            });

            modelBuilder.ConfigureInvoice();
        }
    }
}
