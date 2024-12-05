using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Models;
using Fora.Challenge.Infrastucture.EdgarInfo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fora.Challenge.Infrastucture
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IEdgarApiService, EdgarApiService>(options =>
            {
                options.BaseAddress = new Uri("https://data.sec.gov/");
                options.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.34.0");
                options.DefaultRequestHeaders.Add("Accept", "*/*");
            });

            return services;
        }
    }
}
