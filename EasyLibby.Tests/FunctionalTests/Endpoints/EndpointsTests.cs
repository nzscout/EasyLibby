using Microsoft.AspNetCore.Mvc.Testing;

namespace EasyLibby.Tests.FunctionalTests.Endpoints;

public class EndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public EndpointsTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Theory]
    [InlineData("/books")]
    [InlineData("/authors")]
    [InlineData("/members")]
    //[InlineData("/loans")]
    //[InlineData("/librarians")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        var response = await client.GetAsync(url);
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
    }

}
