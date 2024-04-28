using Ardalis.HttpClientTestExtensions;
using EasyLibby.Api.Data;
using EasyLibby.Api.Entities.BookAggregate;
using EasyLibby.Tests.FunctionalTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace EasyLibby.Tests.FunctionalTests.Endpoints.BookEndpoints;

public class BooksUpdateEndpointTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public BooksUpdateEndpointTest(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task ReturnsNotFoundGivenNonexistingBook()
    {
        int nonexistingBookId = 9991;
        var someBook = SeedData.GetBooks().Last();
        string route = Routes.Books.Update(nonexistingBookId);

        someBook.Title += "_updated";

        await client.PutAndEnsureNotFoundAsync(route, new StringContent(JsonConvert.SerializeObject(someBook.AsUpdateDto()), Encoding.UTF8, "application/json"));
    }

    [Fact]
    public async Task UpdatesExistingBook()
    {
        var existingBook = SeedData.GetBooks().First();
        int existingBookId = existingBook.Id;
        string route = Routes.Books.Update(existingBookId);

        var tempBook = existingBook.AsUpdateDto();

        existingBook.Title += "_updated";
        existingBook.ISBN = 111111;
        existingBook.AuthorId = 3;
        existingBook.PublishedDate = new DateTime(2001, 2, 2);
        existingBook.CoverImageUri = "https://test.com/updated.jpg";

        var response = await client.PutAsync(route,
            new StringContent(JsonConvert.SerializeObject(existingBook.AsUpdateDto()), Encoding.UTF8, "application/json"));

        response.EnsureNoContent();

        //retrieving the updated book
        response = await client.GetAsync(route);
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var retrievedBook = JsonConvert.DeserializeObject<BookDto>(stringResponse);

        Assert.NotNull(retrievedBook);
        Assert.Equal(retrievedBook.Id, existingBook.Id);
        Assert.Equal(retrievedBook.Title, existingBook.Title);
        Assert.Equal(retrievedBook.ISBN, existingBook.ISBN);
        Assert.Equal(retrievedBook.AuthorId, existingBook.AuthorId);
        Assert.Equal(retrievedBook.PublishedDate, existingBook.PublishedDate);
        Assert.Equal(retrievedBook.CoverImageUri, existingBook.CoverImageUri);

        //reversing changes
        response = await client.PutAsync(route,
                       new StringContent(JsonConvert.SerializeObject(tempBook), Encoding.UTF8, "application/json"));
        response.EnsureNoContent();
    }

    [Fact]
    public async Task GivenLongRunningCreateRequest_WhenTokenSourceCallsForCancellation_RequestIsTerminated()
    {
        // Arrange, generate a token source that times out instantly
        var tokenSource = new CancellationTokenSource(TimeSpan.Zero);
        var someBook = SeedData.GetBooks().Last();
        // Act
        var request = client.PostAsync(Routes.Books.Update(2), new StringContent(JsonConvert.SerializeObject(someBook.AsUpdateDto()), Encoding.UTF8, "application/json"), tokenSource.Token);

        // Assert
        await Assert.ThrowsAsync<TaskCanceledException>(async () => await request);
    }

}