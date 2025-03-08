using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using Ticket.Infrastructure.Context;
using Ticket.Test.Utilities;

namespace Ticket.Test;


public class TeamApiTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly TicketDbContext _dbContext;
    private static string _mediaType = "application/json";

    public TeamApiTests(TestingWebAppFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        var token = new Authenticator(_httpClient).Token;
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        using var scope = factory.Services.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
        
    }

    public static IEnumerable<object[]> AddTeamData()
    {
        yield return new object[]
        {
            "AddTeamTest",
            HttpStatusCode.OK,
        };
        yield return new object[]
        {
            "",
            HttpStatusCode.InternalServerError
        };
    }

    // Check Adding Team
    [Theory]
    [MemberData(nameof(AddTeamData))]
    public async void AddTeamApiTest(string teamName, HttpStatusCode expectedResult)
    {
        //var token = await GetAuthenticationTokenAsync();
        //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var content = new {
            Name = teamName,
        };
        var requestUrl = "/team/add";
        var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
        var response = await _httpClient.PostAsync(requestUrl, requestContent);
        Assert.Equal(expectedResult, response.StatusCode);
    }

    public static IEnumerable<object[]> RemoveTeamData()
    {
        
        yield return new object[]
        {
            1,
            HttpStatusCode.OK
        };

        yield return new object[]
        {
            99999,
            HttpStatusCode.InternalServerError
        };
    }

    // Check Removing Team
    [Theory]
    [MemberData(nameof(RemoveTeamData))]
    public async void RemoveTeamTest(int teamId, HttpStatusCode expectedResult)
    {
        var requestUrl = "/team/remove";
        //var existingTeam = _dbContext.Team.FirstOrDefault();
        var content = new
        {
            Id = teamId
        };
        var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
        var response = await _httpClient.PostAsync(requestUrl, requestContent);
        Assert.Equal(expectedResult, response.StatusCode);    
    }

    // Test get teams data
    [Fact]
    public async Task GetTeamsTest()
    {
        var requestUrl = "/team/teams";
        var response = await _httpClient.GetAsync(requestUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.StartsWith("[", responseContent);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // Checks Get teams users Data
    [Fact]
    public async void GetTeamUsers()
    {
        var existingTeam = _dbContext.Team.FirstOrDefault();
        var requestUrl = $"/team/get-users?teamId={existingTeam?.Id}";
        var response = await _httpClient.GetAsync(requestUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.StartsWith("[", responseContent);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}





