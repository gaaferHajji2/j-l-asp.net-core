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

            // using Fluent API
            modelBuilder.Entity<Invoice>(b =>
            {
                b.ToTable("Invoices");
                b.HasKey(i => i.Id);
                b.Property(p => p.Id).HasColumnName("Id");
                b.Property(p => p.InvoiceNumber).HasColumnName("InvoiceNumber").HasColumnType("varchar(32)").IsRequired();
                b.Property(p => p.ContactName).HasColumnName("ContactName").HasMaxLength(32).IsRequired();
                b.Property(p => p.Description).HasColumnName("Description").HasMaxLength(256);
                b.Property(p => p.Amount).HasColumnName("Amount").HasColumnType("decimal(18,2)").IsRequired();
                b.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
                b.Property(p => p.InvoiceDate).HasColumnName("InvoiceDate").HasColumnType("datetimeoffset").IsRequired();
                b.Property(p => p.DueDate).HasColumnName("DueDate").HasColumnType("datetimeoffset").IsRequired();
                b.Property(p => p.Status).HasColumnName("Status").HasMaxLength(16).HasConversion(
                    v => v.ToString(),
                    v => (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), v)
                );
            });
        }
    }
}
