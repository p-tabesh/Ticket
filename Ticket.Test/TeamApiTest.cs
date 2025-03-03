using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Ticket.Test;


public class TeamApiTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private string token;
    private string _mediaType = "application/json";

    public TeamApiTests(TestingWebAppFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    
    private async Task<string> GetAuthenticationTokenAsync()
    {
        var requestUrl = "/account/login";
        var content = new
        {
            Username = "admin",
            Password = "admin@",
            Rule = "admin"
        };
        var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
        var response = await _httpClient.PostAsync(requestUrl,requestContent);
        var responseContent = await response.Content.ReadFromJsonAsync<Dictionary<string,string>>();
        var token = responseContent["token"];
        
        return token;
    }

    [Theory]
    [InlineData("salam",HttpStatusCode.OK)]
    public async void AddTeamApiTest(string teamName, HttpStatusCode expectedResult)
    {
        var token = await GetAuthenticationTokenAsync();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var content = new {
            Name = teamName,
        };
        var requestUrl = "/team/add";
        var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
        var response = await _httpClient.PostAsync(requestUrl, requestContent);
        Assert.Equal(expectedResult, response.StatusCode);
    }    
}

