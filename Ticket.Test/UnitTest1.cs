using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Ticket.Application.Services;
using Ticket.Infrastructure.Context;

namespace Ticket.Test;


public class TicketApiInterationTest : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public TicketApiInterationTest(TestingWebAppFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async void Test()
    {
        var requestUrl = "/ticket/tickets";
        

        var response = await _httpClient.GetAsync(requestUrl);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("UnAuthorized",content);
    }
}