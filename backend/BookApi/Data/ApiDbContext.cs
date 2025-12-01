using Microsoft.EntityFrameworkCore;
using BookApi.Models;



using BookApi.Data;
using BookApi.Models;
using Microsoft.Net.Http.Headers;

namespace BookApi.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base (options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbQuote<User> Quotes => Set<Quote>();
}
