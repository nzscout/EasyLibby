using EasyLibby.Api.Entities.BookAggregate;
using EasyLibby.Api.Extensions;
using EasyLibby.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EasyLibby.Api.Endpoints;

public static class BookEndpoints
{
    const string GetBookEndpointName = "GetBook";
    public static RouteGroupBuilder MapBookEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/books")
                          .WithParameterValidation()
                          .WithOpenApi()
                          .WithTags("books");

        group.MapGet("/", async (
            IRepository<Book> repository,
            ILoggerFactory loggerFactory,
            [AsParameters] GetBooksDto request,
            HttpContext http) =>
        {
            var totalCount = await repository.CountAsync(b => string.IsNullOrWhiteSpace(request.Filter) ? true : b.Title.Contains(request.Filter));
            http.Response.AddPaginationHeader(totalCount, request.PageSize);

            return Results.Ok((await repository.GetAllAsync(
                request.PageNumber,
                request.PageSize,
                b => string.IsNullOrWhiteSpace(request.Filter) ? true : b.Title.Contains(request.Filter)))
                .Select(entity => entity.AsDto()));
        })
        .WithSummary("Gets all books")
        .WithDescription("Gets all books with filtering and pagination");

        group.MapGet("/{id}", async Task<Results<Ok<BookDto>, NotFound>> (IRepository<Book> repository, int id) =>
        {
            Book? book = await repository.GetAsync(id);
            return book is not null ? TypedResults.Ok(book.AsDto()) : TypedResults.NotFound();
        })
        .WithName(GetBookEndpointName)
        .WithSummary("Gets a book by id");

        group.MapPost("/", async Task<CreatedAtRoute<BookDto>> (IRepository<Book> repository, CreateBookDto createEntityDto) =>
        {
            Book entity = createEntityDto.AsEntity();
            await repository.CreateAsync(entity);
            return TypedResults.CreatedAtRoute(entity.AsDto(), GetBookEndpointName, new { id = entity.Id });
        })
        .WithSummary("Creates a new book");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (IRepository<Book> repository, int id, UpdateBookDto updatedEntityDto) =>
        {
            Book? existingEntity = await repository.GetAsync(id);

            if (existingEntity is null)
            {
                return TypedResults.NotFound();
            }

            BookMapper.UpdateBookDtoToBook(updatedEntityDto, existingEntity);
            await repository.UpdateAsync(existingEntity);
            return TypedResults.NoContent();
        })
        .WithSummary("Updates book");

        group.MapDelete("/{id}", async Task<Results<NotFound, NoContent>> (IRepository<Book> repository, int id) =>
        {
            Book? book = await repository.GetAsync(id);

            if (book is not null)
            {
                await repository.DeleteAsync(id);
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        })
        .WithSummary("Deletes a book");

        return group;
    }
}