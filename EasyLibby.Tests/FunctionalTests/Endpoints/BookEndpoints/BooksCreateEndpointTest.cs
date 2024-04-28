using EasyLibby.Api.Data;
using EasyLibby.Api.Entities.BookAggregate;
using EasyLibby.Tests.FunctionalTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace EasyLibby.Tests.FunctionalTests.Endpoints.BookEndpoints
{
    public class BooksCreateEndpointTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        public BooksCreateEndpointTest(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task CreatesANewBook()
        {
            var lastBook = SeedData.GetBooks().Last();
            int existingBookId = lastBook.Id;
            string route = Routes.Books.Delete(existingBookId);
            var response = await client.PostAsync(Routes.Books.Create,
                new StringContent(JsonConvert.SerializeObject(lastBook.AsCreateDto()), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookDto>(stringResponse);

            Assert.NotNull(result);
            Assert.Equal(result.Id, lastBook.Id + 1);
            Assert.Equal(result.Title, lastBook.Title);
            Assert.Equal(result.ISBN, lastBook.ISBN);
            Assert.Equal(result.AuthorId, lastBook.AuthorId);
            Assert.Equal(result.PublishedDate, lastBook.PublishedDate);
            Assert.Equal(result.CoverImageUri, lastBook.CoverImageUri);
        }

        [Fact]
        public async Task GivenLongRunningCreateRequest_WhenTokenSourceCallsForCancellation_RequestIsTerminated()
        {
            // Arrange, generate a token source that times out instantly
            var tokenSource = new CancellationTokenSource(TimeSpan.Zero);
            var lastBook = SeedData.GetBooks().Last();
            // Act
            var request = client.PostAsync(Routes.Books.Create, new StringContent(JsonConvert.SerializeObject(lastBook), Encoding.UTF8, "application/json"), tokenSource.Token);

            // Assert
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await request);
        }

    }
}