using Core.Base;
using Core.Books;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity entity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedBy = "ADMIN";
                        entity.CreatedDate = DateTime.Now;
                        ;
                        break;
                    case EntityState.Modified:
                        entity.ModifiedBy = "ADMIN";
                        entity.ModifiedDate = DateTime.Now;
                        ;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigTableForDb();
        base.OnModelCreating(modelBuilder);
    }
}