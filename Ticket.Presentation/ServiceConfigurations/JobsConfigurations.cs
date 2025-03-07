using Coravel;
using Ticket.Application.Jobs;

namespace Ticket.Presentation.ServiceConfigurations;

public static class JobsConfigurations
{
    public static void AddJobs(this IServiceCollection services)
    {
        services.AddScheduler();
        services.AddTransient<SetCloseForFinishTicketsJob>();
    }
}
