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
                b.HasMany(m => m.Actors).WithMany(a => a.Movies).UsingEntity<MovieActor>(
                    j => j.HasOne(ma => ma.Actor).WithMany(a => a.MovieActors).HasForeignKey(ma => ma.ActorId),
                    j => j.HasOne(ma => ma.Movie).WithMany(m => m.MovieActors).HasForeignKey(ma => ma.MovieId),
                    j =>
                    {
                        j.Property(ma => ma.UpdateTime).HasColumnName(nameof(MovieActor.UpdateTime))
                            .HasDefaultValueSql("CURRENT_TIMESTAMP");
                        j.HasKey(ma => new { ma.MovieId, ma.ActorId});
                    }
                );
            });

        }
    }
}
