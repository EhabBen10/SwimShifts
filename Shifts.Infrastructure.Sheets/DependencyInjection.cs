using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shifts.Application.Interfaces;
using Shifts.Infrastructure.Sheets.Services;
using Shifts.Infrastructure.Sheets.Utility;

namespace Shifts.Infrastructure.Sheets
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGoogleSheets(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleSheetsConfig>(configuration.GetSection(nameof(GoogleSheetsConfig)));
            services.AddScoped<IGoogleSheetsService, GoogleSheetsService>();
            return services;

        }
    }
}