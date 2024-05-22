using Microsoft.EntityFrameworkCore;

namespace PassIn.Infrastructure;

public class PassInDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlite("Data Source=C:\\codes\\chsarp\\PassIn\\PassInDb.db");
        optionsBuilder.UseSqlServer("Server=localhost\\1200;Database=cashFlow;Uid=sa;Pwd=SQLserver01@;Trusted_Connection=True;TrustServerCertificate=True;");

    }
}
//oasdask