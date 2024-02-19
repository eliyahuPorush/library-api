using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Address> Addresses { get; set; }

}