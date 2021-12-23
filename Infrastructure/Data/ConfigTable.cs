using Core.Books;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class ConfigTable
{
    public static void ConfigTableForDb(this ModelBuilder builder)
    {
        builder.Entity<Book>().HasKey(x => x.Id);
        builder.Entity<Book>().Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Entity<Book>().Property(x=>x.BookName).HasMaxLength(250);
    }
}