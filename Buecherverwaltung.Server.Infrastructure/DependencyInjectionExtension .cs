using Buecherverwaltung.Server.Core;
using Buecherverwaltung.Server.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Buecherverwaltung.Server.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BuecherverwaltungDatenbankContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
            services.AddSingleton<DbContext, BuecherverwaltungDatenbankContext>();
            services.AddSingleton<IRepository, Repository>();
        }
    }
}
