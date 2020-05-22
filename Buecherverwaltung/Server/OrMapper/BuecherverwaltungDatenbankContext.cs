using Buecherverwaltung.Server.OrMapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Buecherverwaltung.Server.OrMapper
{
    public class BuecherverwaltungDatenbankContext : DbContext
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
