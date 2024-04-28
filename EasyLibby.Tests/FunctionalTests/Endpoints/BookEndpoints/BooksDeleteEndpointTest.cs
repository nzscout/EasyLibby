using Ardalis.HttpClientTestExtensions;
using EasyLibby.Api.Data;
using EasyLibby.Tests.FunctionalTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EasyLibby.Tests.FunctionalTests.Endpoints.BookEndpoints
{
    public class BooksDeleteEndpointTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        public BooksDeleteEndpointTest(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task DeleteAnExistingBook()
        {
            int existingBookId = 2;
            //Book existingBook = SeedData.GetBooks().First(b => b.Id == existingBookId);
            string route = Routes.Books.Delete(existingBookId);

            var response = await client.DeleteAsync(route);
            response.EnsureSuccessStatusCode();

            response = await client.GetAsync(Routes.Books.Get(existingBookId));
            response.EnsureNotFound();

            //todo add item
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonexistingBook()
        {
            int nonexistingBookId = 9999;
            string route = Routes.Books.Delete(nonexistingBookId);

            await client.DeleteAndEnsureNotFoundAsync(route);
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