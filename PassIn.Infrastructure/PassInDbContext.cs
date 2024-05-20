using Microsoft.EntityFrameworkCore;

namespace PassIn.Infrastructure;

public class PassInDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\codes\\chsarp\\PassIn\\PassInDb.db");
    }
}
