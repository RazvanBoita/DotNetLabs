using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books");
            
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Author)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.ISBN)
                .IsRequired()
                .HasMaxLength(13);

            entity.Property(e => e.PublicationDate).IsRequired();
        });
        // base.OnModelCreating(modelBuilder);
    }
    
}