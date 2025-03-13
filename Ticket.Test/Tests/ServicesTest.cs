using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Ticket.Test.Tests;

public class ServicesTest : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private static string _mediaType = "application/json";

    public ServicesTest(TestingWebAppFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    private record LoginModel(string Token);

    [Fact]
    public async void Check_Admin_Team_Service()
    {
        //Check Login Admin
        string loginToken = "";

        string requestUrl = "/account/login";
        {
            var content = new
            {
                Username = "admin",
                Password = "admin@111"
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
            var response = await _httpClient.PostAsync(requestUrl, requestContent);
            var reply = await response.Content.ReadFromJsonAsync<LoginModel>();
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            reply.Should().NotBeNull();
            reply.Token.Should().NotBeNullOrWhiteSpace();

            loginToken = reply.Token;
        }

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginToken}");

        requestUrl = "/team/add";
        // Check not valid Adding Team    
        {
            var content = new
            {
                Name = "",
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
            var response = await _httpClient.PostAsync(requestUrl, requestContent);
            var reply = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        int teamId = 0;
        // Check valid Adding Team    
        {
            var content = new
            {
                Name = "AddTeamTest",
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, _mediaType);
            var response = await _httpClient.PostAsync(requestUrl, requestContent);
            var reply = await response.Content.ReadAsStringAsync();
            int.TryParse(reply, out teamId);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // Check Get teams users Data        
        requestUrl = $"/team/teams/{teamId}";
        {
            var response = await _httpClient.GetAsync(requestUrl);
            var reply = await response.Content.ReadAsStringAsync();            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // Check get teams data    
        requestUrl = "/team/teams";
        {
            var response = await _httpClient.GetAsync(requestUrl);
            var reply = await response.Content.ReadAsStringAsync();
            reply.Should().StartWith("[").And.EndWith("]");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // Check Removing Team                    
        requestUrl = $"/team/remove/{teamId}";
        {
            var response = await _httpClient.DeleteAsync(requestUrl);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // Check Logout
        requestUrl = "/account/logout";
        {                        
            var response = await _httpClient.GetAsync(requestUrl);            

            response.StatusCode.Should().Be(HttpStatusCode.OK);                        
        }

        // Check BlackList After Logout    
        requestUrl = "/team/teams";
        {
            var response = await _httpClient.GetAsync(requestUrl);
            var reply = await response.Content.ReadAsStringAsync();            
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}

