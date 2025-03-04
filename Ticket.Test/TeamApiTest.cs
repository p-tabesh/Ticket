using System.Net;
using System.Text;
using System.Text.Json;
using Ticket.Test.Utilities;

namespace Ticket.Test;


public class TeamApiTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private string _mediaType = "application/json";

    public TeamApiTests(TestingWebAppFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        var token = new Authenticator(_httpClient).Token;
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    [Theory]
    [InlineData("tesstttttttt",HttpStatusCode.OK)]
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

    [Theory]
    [InlineData(1,HttpStatusCode.OK)]
    public async void RemoveTeamTest(int teamId, HttpStatusCode expectedResult)
    {
        var requestUrl = "/team/remove";
        var content = new
        {
            Id = 2
        };
        var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
        var response = await _httpClient.PostAsync(requestUrl, requestContent);
        Assert.Equal(expectedResult, response.StatusCode);    
    }


}

