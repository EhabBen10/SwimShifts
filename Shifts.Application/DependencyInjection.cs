using Shifts.Application.Interfaces;
using Shifts.Application.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IExportToDB, ExportToDB>();
        return services;
    }
}