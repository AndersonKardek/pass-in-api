using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure;

public class PassInDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1200;Database=check-in;User Id=sa;Password=SQLserver01@;TrustServerCertificate=True;");
    }
}
