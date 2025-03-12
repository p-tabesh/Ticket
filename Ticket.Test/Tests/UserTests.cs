using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using Ticket.Test.Utilities;

namespace Ticket.Test.Tests;

public class UserTests:IClassFixture<TestingWebAppFactory<Program>>
{
    private TicketDbContext _dbContext;
    private HttpClient _httpClient;
    private IServiceScope _scope;
    private static string _token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsImtpZCI6ImE4YTFiMmM4LTkzNDYtNDhjNi1hNDJlLWFhZDMyZjI2OWU0NCIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOlsiNCIsIjQiXSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJleHAiOjE3NDE3NTA3MTEsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.7vb6MDzTswQ0pTudZWJZKEejrM1DRok2TYIRh1Nyjj4";
    public UserTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<TicketDbContext>();
    }

    public void Dispose()
    {
        _dbContext.Users.RemoveRange(_dbContext.Users.ToList());
        _dbContext.SaveChanges();
        _dbContext.Dispose();
        _scope.Dispose();
    }


    [Theory]
    [InlineData("pooya","admin@123","test@gmail.com",HttpStatusCode.OK)]
    [InlineData("pooya","admin@123","test@gmail.com", HttpStatusCode.InternalServerError)]
    [InlineData("pooya","admin@123","test@gmail.com", HttpStatusCode.InternalServerError)]
    public async Task AddUserTest(string username, string password, string email, HttpStatusCode expectedStatusCode)
    {
        var requestUrl = "/user/add-user";
        var existingTeam = _dbContext.Team.FirstOrDefault();
        var content = new
        {
            Username = username,
            Password = password,
            Email = email,
            TeamId = existingTeam.Id
        };
        var requestContent = ContentConverter.ConvertToStringContent(content);
        var response = await _httpClient.PostAsync(requestUrl, requestContent);

        response.StatusCode.Should().Be(expectedStatusCode);
    }
}
