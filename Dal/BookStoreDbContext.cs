using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class BookStoreDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public BookStoreDbContext(DbContextOptions options) : base(options)
    {
    }
}