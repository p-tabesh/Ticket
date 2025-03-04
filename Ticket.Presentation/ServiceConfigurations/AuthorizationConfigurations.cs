namespace Ticket.Presentation.ServiceConfigurations;

public static class AuthorizationConfigurations
{
    public static void AddAuthorizations(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
        });
    }
}
