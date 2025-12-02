using JLokaTestEFCore.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestEFCore.Data
{
    public static class ContactModelCreatingExtensions
    {
        public static void ConfigureContact(this ModelBuilder builder)
        {
            builder.Entity<Contact>(b =>
            {
                b.ToTable("Contacts");
                b.HasKey(p => p.Id);
                b.Property(p => p.Id).HasColumnName(nameof(Contact.Id));
                b.Property(p => p.FirstName).HasColumnName(nameof(Contact.FirstName)).HasMaxLength(64).IsRequired();
                b.Property(p => p.LastName).HasColumnName(nameof(Contact.LastName)).HasMaxLength(256).IsRequired();
                b.Property(p => p.Title).HasColumnName(nameof(Contact.Title)).HasMaxLength(256);
                b.Property(p => p.Email).HasColumnName(nameof(Contact.Email)).HasMaxLength(256).IsRequired();
                b.Property(p => p.Phone).HasColumnName(nameof(Contact.Phone)).HasMaxLength(256).IsRequired();
            });
        }
    }
}
