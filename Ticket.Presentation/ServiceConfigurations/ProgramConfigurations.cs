using Ticket.Application.Services;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Repository;

namespace Ticket.Presentation.ServiceConfigurations;

public static class ServiceConfigurations
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryFieldRepository, CategoryFieldRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IFieldRepository, FieldRepository>();
    }
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<AccountService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<TicketService>();
        services.AddScoped<UserService>();
        services.AddScoped<TeamService>();
    }
}
