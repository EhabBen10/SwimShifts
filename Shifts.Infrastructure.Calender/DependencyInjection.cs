using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shifts.Infrastructure.Calender.Services;

namespace Shifts.Infrastructure.Calender
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCalendarServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleSheetsConfig>(configuration.GetSection(nameof(GoogleSheetsConfig)));
            services.AddScoped<IGoogleCalendarService, GoogleCalendarService>();
            return services;
        }
    }
}
