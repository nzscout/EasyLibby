using System.ComponentModel.DataAnnotations;

namespace EasyLibby.Api.Entities.MemberAggregate
{
    public record MemberDto(
        int Id,
        [Required] string FirstName,
        [Required] string LastName,
        DateTime? DateBirth,
        string Email
    );

    public record GetMembersDto(
        int PageNumber = 1,
        int PageSize = 10,
        string? Filter = null
    );

    public record CreateMemberDto(
        [Required] string FirstName,
        [Required] string LastName,
        DateTime? DateBirth,
        string Email,
        string Password
    );

    public record UpdateMemberDto(
        [Required] string FirstName,
        [Required] string LastName,
        DateTime? DateBirth,
        string Email,
        string Password
    );

}
