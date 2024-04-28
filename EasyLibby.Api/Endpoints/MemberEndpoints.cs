using EasyLibby.Api.Entities.BookAggregate;
using EasyLibby.Api.Entities.MemberAggregate;
using EasyLibby.Api.Extensions;
using EasyLibby.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EasyLibby.Api.Endpoints;

public static class MemberEndpoints
{
    const string GetMemberEndpointName = "GetMember";
    public static RouteGroupBuilder MapMemberEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/members")
                          .WithParameterValidation()
                          .WithOpenApi()
                          .WithTags("members");

        group.MapGet("/", async (
            IRepository<Member> repository,
            ILoggerFactory loggerFactory,
            [AsParameters] GetMembersDto request,
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
        .WithSummary("Gets all Members")
        .WithDescription("Gets all Members with filtering and pagination");

        group.MapGet("/{id}", async Task<Results<Ok<MemberDto>, NotFound>> (IRepository<Member> repository, int id) =>
        {
            Member? Member = await repository.GetAsync(id);
            return Member is not null ? TypedResults.Ok(Member.AsDto()) : TypedResults.NotFound();
        })
        .WithName(GetMemberEndpointName)
        .WithSummary("Gets an Member by id");

        group.MapPost("/", async Task<CreatedAtRoute<MemberDto>> (IRepository<Member> repository, CreateMemberDto createEntityDto) =>
        {
            Member entity = createEntityDto.AsEntity();
            await repository.CreateAsync(entity);
            return TypedResults.CreatedAtRoute(entity.AsDto(), GetMemberEndpointName, new { id = entity.Id });
        })
        .WithSummary("Creates a new Member");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (IRepository<Member> repository, int id, UpdateMemberDto updatedEntityDto) =>
        {
            Member? existingEntity = await repository.GetAsync(id);

            if (existingEntity is null)
            {
                return TypedResults.NotFound();
            }

            MemberMapper.UpdateMemberDtoToMember(updatedEntityDto, existingEntity);
            await repository.UpdateAsync(existingEntity);
            return TypedResults.NoContent();
        })
        .WithSummary("Updates Member");

        group.MapDelete("/{id}", async Task<Results<NotFound, NoContent>> (IRepository<Member> repository, int id) =>
        {
            Member? Member = await repository.GetAsync(id);

            if (Member is not null)
            {
                await repository.DeleteAsync(id);
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        })
        .WithSummary("Deletes a Member");

        return group;
    }
}