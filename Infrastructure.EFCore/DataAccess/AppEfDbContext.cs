using Core.Base;
using Core.Books;
using Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.DataAccess;

public class AppEfDbContext<Tkey> : DbContext
{
    protected AppEfDbContext()
    {
    }

    public AppEfDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book<Tkey>> Books { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity<Tkey> entity)
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