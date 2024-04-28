using Ardalis.HttpClientTestExtensions;
using EasyLibby.Api.Data;
using EasyLibby.Api.Entities.BookAggregate;
using EasyLibby.Tests.FunctionalTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EasyLibby.Tests.FunctionalTests.Endpoints.BookEndpoints
{
    public class BooksListEndpointTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        public BooksListEndpointTest(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsAllBooks()
        {
            var result = await client.GetAndDeserializeAsync<IEnumerable<BookDto>>(Routes.Books.List());
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GivenLongRunningGetRequest_WhenTokenSourceCallsForCancellation_RequestIsTerminated()
        {
            // Arrange, generate a token source that times out instantly
            var tokenSource = new CancellationTokenSource(TimeSpan.Zero);
            var firstBook = SeedData.GetBooks().First();

            // Act
            var request = client.GetAsync(Routes.Books.List(), tokenSource.Token);

            // Assert
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await request);
        }

    }
}