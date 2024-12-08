using Fora.Challenge.Api.Middleware;
using Fora.Challenge.Application;
using Fora.Challenge.Infrastucture;
using Fora.Challenge.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fora.Challenge.Api
{
    public static class StartupExtensions
    {
        /// <summary>Configures the services.</summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The application.</returns>
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            return builder.Build();
        }

        /// <summary>Configures the pipeline.</summary>
        /// <param name="app">The application.</param>
        /// <returns>The application.</returns>
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.MapControllers();

            return app;
        }

        // todo: this can be removed
        /// <summary>Resets the database asynchronous.</summary>
        /// <param name="app">The application.</param>
        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<CompanyDataDbContext>();
                if (context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception)
            {
                //add logging here later on
            }
        }
    }
}
