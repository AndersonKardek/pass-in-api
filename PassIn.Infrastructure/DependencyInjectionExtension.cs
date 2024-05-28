using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PassIn.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionSqlServer");

        services.AddDbContext<PassInDbContext>(config => config.UseSqlServer(connectionString));
    }
}
