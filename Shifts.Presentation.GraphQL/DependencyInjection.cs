using Microsoft.Extensions.DependencyInjection;
using Shifts.Application.Interfaces;
using Shifts.Presentation.GraphQL.Queries;

namespace Shifts.Presentation.GraphQL;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the services related to GraphQL
    /// </summary>
    /// <param name="services">The Service collection</param>
    /// <returns>The Service collection</returns>
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        services.AddGraphQLServer()
          .RegisterService<IGoogleSheetsService>()
          .RegisterService<IGoogleCalendarService>()
          .RegisterService<IGoogleAuthService>()
          .AddQueryType(q => q.Name("Query"))
          .AddTypeExtension<EventsQuery>();

        return services;
    }

}