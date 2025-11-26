using JLokaTestEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestEFCore.Data
{
    public static class InvoiceItemModelCreatingExtensions
    {
        public static void ConfigureInvoiceItem(this ModelBuilder builder)
        {
            builder.Entity<InvoiceItem>(b =>
            {
                b.ToTable("InvoiceItems");
                b.Property(p => p.Id).HasColumnName(nameof(InvoiceItem.Id));
                b.Property(p => p.Name).HasColumnName(nameof(InvoiceItem.Name)).HasMaxLength(64).IsRequired();
                b.Property(p => p.Description).HasColumnName(nameof(InvoiceItem.Description)).HasMaxLength(256);
                b.Property(p => p.UnitPrice).HasColumnName(nameof(InvoiceItem.UnitPrice)).HasColumnType("decimal(8,2)").HasPrecision(8, 2);
                b.Property(p => p.Quantity).HasColumnName(nameof(InvoiceItem.Quantity)).HasColumnType("decimal(8,2)").HasPrecision(8, 2);
                b.Property(p => p.Amount).HasColumnName(nameof(InvoiceItem.Amount)).HasColumnType("decimal(18,2)").HasPrecision(18, 2);
                b.Property(p => p.InvoiceId).HasColumnName(nameof(InvoiceItem.InvoiceId));
            });
        }
    }
}
