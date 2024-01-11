using Shifts.Application.Interfaces;
using Shifts.Infrastructure.Excel.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddExcelService(this IServiceCollection services)
    {
        services.AddScoped<ICreateExcel, CreteExcel>();
        return services;
    }
}