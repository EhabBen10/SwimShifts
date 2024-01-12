
using Shifts.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Shifts.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<WaterSampleContext>());

        services.AddDbContext<WaterSampleContext>(builder =>
            builder.UseSqlServer(configuration.GetConnectionString("DB"),
              sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }
            ));
        return services;
    }
}
