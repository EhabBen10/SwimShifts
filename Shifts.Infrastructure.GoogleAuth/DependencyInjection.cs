using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shifts.Infrastructure.GoogleAuth
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGoogleAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleSheetsConfig>(configuration.GetSection(nameof(GoogleSheetsConfig)));
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();

            return services;
        }
    }
}
