using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Ticket.Test.Utilities;

public class Authenticator
{
    private HttpClient _httpClient;
    private string _token;
    public Authenticator(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public string Token
    {
        get
        {
            if (_token == null)
            {
                
                _token = GetAuthenticationTokenAsync().Result;
                return _token;
            }
            return _token;
        }
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
        var requestContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(requestUrl, requestContent);
        var responseContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        var token = responseContent["token"];

        return token;
    }
}
