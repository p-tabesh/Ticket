using Microsoft.EntityFrameworkCore;
using Ticket.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;






public class TestingWebAppFactory<TStartup>: WebApplicationFactory<Program> where TStartup : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(service =>
        {
            var descriptor = service.FirstOrDefault(d => d.ServiceType == typeof(DbContextOptions<TicketDbContext>));
            if (descriptor != null)
                service.Remove(descriptor);

            var connection = new SqliteConnection("FileName=TicketingTest.db");
            connection.Open();
            service.AddDbContext<TicketDbContext>(options =>
            {
                options.UseSqlite(connection);
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


