using Buecherverwaltung.Server.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Buecherverwaltung.Server.Infrastructure.Database
{
    internal class BuecherverwaltungDatenbankContext : DbContext
    {
        public virtual DbSet<Book> Books { get; set; }

        public BuecherverwaltungDatenbankContext(DbContextOptions<BuecherverwaltungDatenbankContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ArticleNumber);

                entity.Property(e => e.ArticleNumber)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.IsLoaned).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
