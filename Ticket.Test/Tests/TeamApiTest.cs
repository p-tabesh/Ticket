using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;



namespace Ticket.Test.Tests;


public class TeamApiTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly IServiceScope _scope;
    private readonly TicketDbContext _dbContext;
    private static string _token = "";
    private static string _mediaType = "application/json";

    public TeamApiTests(TestingWebAppFactory<Program> factory)
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
        var content = new
        {
            Name = teamName,
        };
        var requestUrl = "/team/add";
        var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
        var response = await _httpClient.PostAsync(requestUrl, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(expectedResult);
    }

    // Checks Get teams users Data
    [Fact]
    public async void GetTeamUsers()
    {
        var requestUrl = $"/team/get-users?teamId=1";
        var response = await _httpClient.GetAsync(requestUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().StartWith("[").And.EndWith("]");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
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


    //public static IEnumerable<object[]> RemoveTeamData()
    //{

    //    yield return new object[]
    //    {
    //        1,
    //        HttpStatusCode.OK,
    //        "success"
    //    };

    //    yield return new object[]
    //    {
    //        99,
    //        HttpStatusCode.InternalServerError,
    //        "doesnt exists",
    //    };
    //}

    //// Check Removing Team
    //[Theory]
    //[MemberData(nameof(RemoveTeamData))]
    //public async void RemoveTeamTest(int cofficient, HttpStatusCode expectedStatusCode, string expectedResponse)
    //{
    //    var requestUrl = "/team/remove";
    //    var existingTeam = _dbContext.Team.FirstOrDefault();
    //    var content = new
    //    {
    //        Id = existingTeam.Id * cofficient
    //    };
    //    var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
    //    var response = await _httpClient.PostAsync(requestUrl, requestContent);
    //    var responseContent = await response.Content.ReadAsStringAsync();
    //    responseContent.Should().Contain(expectedResponse);
    //    response.StatusCode.Should().Be(expectedStatusCode);
    //}
}

