using Microsoft.EntityFrameworkCore;
using minimal_api.Models;

namespace minimal_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; } = null!;
}
