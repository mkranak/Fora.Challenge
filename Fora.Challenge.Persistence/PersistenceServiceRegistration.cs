using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fora.Challenge.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<CompanyDataDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CompanyDataConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICompanyDataRepository, CompanyDataRepository>();

            return services;
        }
    }
}
