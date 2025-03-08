using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using Ticket.Infrastructure.Context;
using FluentAssertions;

namespace Ticket.Test;


public class TeamApiTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly IServiceScope _scope;
    private readonly TicketDbContext _dbContext;
    private static string _token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsImtpZCI6ImM0MDQ2MWQ0LTY3ZDItNGY1Zi05MWUyLWFlZmVmMDYwZGVmMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOlsiNCIsIjQiXSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJleHAiOjE3NDE1MTQyNTAsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.4gfn9nEIEnCBQ7VCrYvl7UejICC4pJIiMj-WRgPb5Ew";
    private static string _mediaType = "application/json";

    public TeamApiTests(TestingWebAppFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<TicketDbContext>();
    }

    public static IEnumerable<object[]> AddTeamData()
    {
        yield return new object[]
        {
            "AddTeamTests",
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
        var content = new {
            Name = teamName,
        };
        var requestUrl = "/team/add";
        var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
        var response = await _httpClient.PostAsync(requestUrl, requestContent);
        var responseContent =await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(expectedResult);
    }

    // Test get teams data
    [Fact]
    public async Task GetTeamsTest()
    {
        var requestUrl = "/team/teams";
        var response = await _httpClient.GetAsync(requestUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().StartWith("[").And.EndWith("]");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // Checks Get teams users Data
    [Fact]
    public async void GetTeamUsers()
    {
        var existingTeam = _dbContext.Team.FirstOrDefault();
        var requestUrl = $"/team/get-users?teamId={existingTeam.Id}";
        var response = await _httpClient.GetAsync(requestUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().StartWith("[").And.EndWith("]");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public static IEnumerable<object[]> RemoveTeamData()
    {
        
        yield return new object[]
        {
            HttpStatusCode.OK,
            "success"
        };

        yield return new object[]
        {
            HttpStatusCode.InternalServerError,
            "doesnt exists",
        };
    }

    // Check Removing Team
    [Theory]
    [MemberData(nameof(RemoveTeamData))]
    public async void RemoveTeamTest(HttpStatusCode expectedStatusCode, string expectedResponse)
    {
        var requestUrl = "/team/remove";
        var existingTeam = _dbContext.Team.FirstOrDefault();
        var content = new
        {
            Id = existingTeam.Id
        };
        var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
        var response = await _httpClient.PostAsync(requestUrl, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().Contain(expectedResponse);
    }

    

    //[Fact]
    //public void Dispose()
    //{
    //    _dbContext.Team.RemoveRange(_dbContext.Team.ToList());
    //    _dbContext.SaveChanges();
    //    _dbContext.Dispose();
    //    _scope.Dispose();
    //}
}





