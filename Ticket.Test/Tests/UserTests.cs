using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace Ticket.Test.Tests;

public class UserTests:IClassFixture<TestingWebAppFactory<Program>>
{
    private TicketDbContext _dbContext;
    private HttpClient _httpClient;
    private IServiceScope _scope;
    private static string _token = "";
    public UserTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<TicketDbContext>();
        
    }
    public void Dispose()
    {
        _dbContext.Team.RemoveRange(_dbContext.Team.ToList());
        _dbContext.SaveChanges();
        _dbContext.Dispose();
        _scope.Dispose();
    }

}
