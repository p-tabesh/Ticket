using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Ticket.Application.Services;
using Ticket.Domain.IService;
using Ticket.Presentation.Middlewares;

namespace Ticket.Presentation.ServiceConfigurations;

public static class ServiceConfigurations
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<AccountService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<TicketService>();
        services.AddScoped<UserService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddTransient<JwtMiddleware>();
        services.AddSingleton<IDistributedCache, RedisCache>();
    }
}
