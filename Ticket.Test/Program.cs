using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Ticket.Domain.Entity;

public class TestingWebAppFactory<TStartup> : WebApplicationFactory<Program> where TStartup : Program
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

                SeedDatabase(db);
            }
        });
    }
    void SeedDatabase(TicketDbContext db)
    {
        //team
        var team = new Team("Test");
        db.Team.Add(team);
        db.SaveChanges();

        db.User.Add(new User("admin", "tV3NBjMRnyBUHNqlMznnwqvVXETW1rQXugBUSZ3PSSo=", "admin@gmail.com", team.Id, true));        
        db.SaveChanges();
    }
}


