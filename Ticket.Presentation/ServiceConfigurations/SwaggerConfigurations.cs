using Microsoft.OpenApi.Models;

namespace Ticket.Presentation.ServiceConfigurations;

public static class SwaggerConfigurations
{
    public static void AddSwaggerConfigurations(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "Jwt Bearer Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }       
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}
