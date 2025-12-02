using JLokaTestEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestEFCore.Data
{
    public static class AddressModelCreatingExtensions
    {
        public static void ConfigureAddress(this ModelBuilder builder)
        {
            builder.Entity<Address>(b =>
            {
                b.ToTable("Addresses");
                b.HasKey(p => p.Id);
                b.Property(p => p.Id).HasColumnName(nameof(Address.Id));
                b.Property(p => p.Street).HasColumnName(nameof(Address.Street)).HasMaxLength(64).IsRequired();
                b.Property(p => p.City).HasColumnName(nameof(Address.City)).HasMaxLength(256).IsRequired();
                b.Property(p => p.State).HasColumnName(nameof(Address.State)).HasMaxLength(256).IsRequired();
                b.Property(p => p.ZipCode).HasColumnName(nameof(Address.ZipCode)).HasMaxLength(256).IsRequired();
                b.Property(p => p.Country).HasColumnName(nameof(Address.Country)).HasMaxLength(256).IsRequired();
                b.Property(p => p.ContactId).HasColumnName(nameof(Address.ContactId));
                b.Ignore(a => a.Contact);
                b.HasOne(a => a.Contact).WithOne(c => c.Address).HasForeignKey<Address>(a => a.ContactId);
            });
        }
    }
}
