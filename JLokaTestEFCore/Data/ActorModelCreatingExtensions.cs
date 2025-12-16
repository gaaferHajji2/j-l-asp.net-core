using JLokaTestEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestEFCore.Data
{
    public static class ActorModelCreatingExtensions
    {
        public static void ConfigureActor(this ModelBuilder builder)
        {
            builder.Entity<Actor>(b =>
            {
                b.ToTable("Actors");
                b.HasKey(i => i.Id);
                b.Property(p => p.Id).HasColumnName(nameof(Actor.Id));
                b.Property(p => p.Name).HasColumnName(nameof(Actor.Name)).HasMaxLength(255).IsRequired();
                b.HasIndex(p => p.Name).IsUnique();
            });
        }
    }
}
