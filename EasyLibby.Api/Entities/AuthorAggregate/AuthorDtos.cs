using System.ComponentModel.DataAnnotations;

namespace EasyLibby.Api.Entities.AuthorAggregate
{
    public record AuthorDto(
        int Id,
        [Required] string FirstName,
        [Required] string LastName,
        DateTime? DateBirth
    );

    public record GetAuthorsDto(
        int PageNumber = 1,
        int PageSize = 10,
        string? Filter = null
    );

    public record CreateAuthorDto(
        [Required] string FirstName,
        [Required] string LastName,
        DateTime? DateBirth
    );

    public record UpdateAuthorDto(
        [Required] string FirstName,
        [Required] string LastName,
        DateTime? DateBirth
    );

}
