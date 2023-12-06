using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shifts.Application.Interfaces;
using Shifts.Infrastructure.Sheets.Services;

namespace Shifts.Infrastructure.Sheets
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGoogleSheets(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGoogleSheetsService, GoogleSheetsService>();

            return services;
        }
    }
}