using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Models;
using Fora.Challenge.Infrastucture.EdgarInfo;
using Fora.Challenge.Infrastucture.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fora.Challenge.Infrastucture
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddHttpClient<IEdgarApiService, EdgarApiService>();

            services.AddTransient<IEdgarApiService, EdgarApiService>();
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
