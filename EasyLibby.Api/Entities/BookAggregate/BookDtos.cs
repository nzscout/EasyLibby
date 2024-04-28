using System.ComponentModel.DataAnnotations;

namespace EasyLibby.Api.Entities.BookAggregate
{
    public record BookDto(
        int Id,
        ulong? ISBN,
        [Required] string Title,
        int AuthorId,
        DateTime? PublishedDate,
        [Url][StringLength(250)] string? CoverImageUri
    );

    public record GetBooksDto(
        int PageNumber = 1,
        int PageSize = 10,
        string? Filter = null
    );

    public record CreateBookDto(
        ulong? ISBN,
        [Required][StringLength(255)] string Title,
        [Required] int AuthorId,
        DateTime? PublishedDate,
        [Url][StringLength(250)] string CoverImageUri
    );

    public record UpdateBookDto(
        ulong? ISBN,
        [Required][StringLength(255)] string Title,
        [Required] int AuthorId,
        DateTime? PublishedDate,
        [Url][StringLength(250)] string CoverImageUri
    );

    public record CoverUploadDto(string BlobUri);
}
