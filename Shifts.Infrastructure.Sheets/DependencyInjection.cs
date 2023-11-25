using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shifts.Application.Interfaces;
using Shifts.Infrastructure.Sheets.Services;

namespace Shifts.Infrastructure.Sheets
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGoogleSheets(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleSheetsConfig>(configuration.GetSection(nameof(GoogleSheetsConfig)));
            var googleSheetsConfigSection = configuration.GetSection(nameof(GoogleSheetsConfig));

            services.AddHttpClient<IGoogleSheetsService, GoogleSheetsService>((provider, client) =>
            {
                var config = provider.GetRequiredService<IOptions<GoogleSheetsConfig>>().Value;
                client.BaseAddress = new Uri(config.Installed.AuthUri);
            });

            return services;
        }
    }
}