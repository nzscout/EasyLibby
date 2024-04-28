using Ardalis.HttpClientTestExtensions;
using EasyLibby.Api.Data;
using EasyLibby.Api.Entities.BookAggregate;
using EasyLibby.Tests.FunctionalTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;

namespace EasyLibby.Tests.FunctionalTests.Endpoints.BookEndpoints
{
    public class BooksGetByIdEndpointTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        public BooksGetByIdEndpointTest(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async void GetAllBooksTest()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var response = await client.GetAsync("/books");
            var data = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenInvalidBookId()
        {
            int invalidId = 9999;
            var response = await client.GetAsync(Routes.Books.Get(invalidId));
            response.EnsureNotFound();
        }

        [Fact]
        public async Task ReturnsBookById()
        {
            var firstBook = SeedData.GetBooks().First();

            var response = await client.GetAsync(Routes.Books.Get(firstBook.Id));
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookDto>(stringResponse);

            Assert.NotNull(result);
            Assert.Equal(firstBook.Id, result.Id);
            Assert.Equal(firstBook.Title, result.Title);
            Assert.Equal(firstBook.ISBN, result.ISBN);
            Assert.Equal(firstBook.AuthorId, result.AuthorId);
            Assert.Equal(firstBook.PublishedDate, result.PublishedDate);
            Assert.Equal(firstBook.CoverImageUri, result.CoverImageUri);
        }

        [Fact]
        public async Task GivenLongRunningGetRequest_WhenTokenSourceCallsForCancellation_RequestIsTerminated()
        {
            // Arrange, generate a token source that times out instantly
            var tokenSource = new CancellationTokenSource(TimeSpan.Zero);
            var firstBook = SeedData.GetBooks().First();

            // Act
            var request = client.GetAsync(Routes.Books.Get(firstBook.Id), tokenSource.Token);

            // Assert
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await request);
        }
    }
}