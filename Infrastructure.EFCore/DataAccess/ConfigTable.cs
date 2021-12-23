using Core.Books;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Data;

public static class ConfigTable
{
    public static void ConfigTableForDb(this ModelBuilder builder)
    {
        builder.Entity<Book<long>>().HasKey(x => x.Id);
        builder.Entity<Book<long>>().Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Entity<Book<long>>().Property(x=>x.BookName).HasMaxLength(250);
    }
}