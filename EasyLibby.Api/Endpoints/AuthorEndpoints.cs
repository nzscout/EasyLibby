using EasyLibby.Api.Entities.AuthorAggregate;
using EasyLibby.Api.Extensions;
using EasyLibby.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EasyLibby.Api.Endpoints;

public static class AuthorEndpoints
{
    const string GetAuthorEndpointName = "GetAuthor";
    public static RouteGroupBuilder MapAuthorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/authors")
                          .WithParameterValidation()
                          .WithOpenApi()
                          .WithTags("authors");

        group.MapGet("/", async (
            IRepository<Author> repository,
            ILoggerFactory loggerFactory,
            [AsParameters] GetAuthorsDto request,
            HttpContext http) =>
        {
            var totalCount = await repository.CountAsync(b =>
                string.IsNullOrWhiteSpace(request.Filter) ? true : b.FirstName.Contains(request.Filter) || b.LastName.Contains(request.Filter));
            http.Response.AddPaginationHeader(totalCount, request.PageSize);

            return Results.Ok((await repository.GetAllAsync(
                request.PageNumber,
                request.PageSize,
                b => string.IsNullOrWhiteSpace(request.Filter) ? true : b.FirstName.Contains(request.Filter) || b.LastName.Contains(request.Filter)))
                .Select(entity => entity.AsDto()));
        })
        .WithSummary("Gets all authors")
        .WithDescription("Gets all authors with filtering and pagination");

        group.MapGet("/{id}", async Task<Results<Ok<AuthorDto>, NotFound>> (IRepository<Author> repository, int id) =>
        {
            Author? Author = await repository.GetAsync(id);
            return Author is not null ? TypedResults.Ok(Author.AsDto()) : TypedResults.NotFound();
        })
        .WithName(GetAuthorEndpointName)
        .WithSummary("Gets an author by id");

        group.MapPost("/", async Task<CreatedAtRoute<AuthorDto>> (IRepository<Author> repository, CreateAuthorDto createEntityDto) =>
        {
            Author entity = createEntityDto.AsEntity();
            await repository.CreateAsync(entity);
            return TypedResults.CreatedAtRoute(entity.AsDto(), GetAuthorEndpointName, new { id = entity.Id });
        })
        .WithSummary("Creates a new Author");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (IRepository<Author> repository, int id, UpdateAuthorDto updatedEntityDto) =>
        {
            Author? existingEntity = await repository.GetAsync(id);

            if (existingEntity is null)
            {
                return TypedResults.NotFound();
            }

            AuthorMapper.UpdateAuthorDtoToAuthor(updatedEntityDto, existingEntity);
            await repository.UpdateAsync(existingEntity);
            return TypedResults.NoContent();
        })
        .WithSummary("Updates Author");

        group.MapDelete("/{id}", async Task<Results<NotFound, NoContent>> (IRepository<Author> repository, int id) =>
        {
            Author? Author = await repository.GetAsync(id);

            if (Author is not null)
            {
                await repository.DeleteAsync(id);
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        })
        .WithSummary("Deletes a Author");

        return group;
    }
}