using JLokaTestEFCore.enums;
using JLokaTestEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestEFCore.Data
{
    public static class MovieModelCreatingExtensions
    {
        public static void ConfigureMovie(this ModelBuilder builder)
        {
            builder.Entity<Movie>(b =>
            {
                b.ToTable("Movies");
                b.HasKey(i => i.Id);
                b.Property(p => p.Id).HasColumnName("Id");
                b.Property(p => p.Title).HasColumnName(nameof(Movie.Title)).HasMaxLength(255).IsRequired();
                b.Property(p => p.Description).HasColumnName(nameof(Movie.Description));
                b.Property(p => p.ReleaseYear).HasColumnName(nameof(Movie.ReleaseYear)).IsRequired();
            });

        }
    }
}
