using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Ticket.Application.Extentions;
using Ticket.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing ;
using Ticket.Test;
using Microsoft.Extensions.DependencyInjection;






public class TestingWebAppFactory<TStartup>: WebApplicationFactory<Program> where TStartup : Program
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(service =>
        {
            var descriptor = service.FirstOrDefault(d => d.ServiceType == typeof(TicketDbContext));
            if (descriptor != null)
                service.Remove(descriptor);




            service.AddDbContext<TicketDbContext>(option =>
            {
                option.UseSqlite("Filename=TicketingTestDatabase.db");
            });



            var serviceProvider = service.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TicketDbContext>();
                db.Database.EnsureCreated();
            }
            
        });
        
    }
}


