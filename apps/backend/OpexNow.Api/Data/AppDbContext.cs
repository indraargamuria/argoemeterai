using Microsoft.EntityFrameworkCore;
using OpexNow.Api.Models;

namespace OpexNow.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
}