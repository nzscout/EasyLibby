using EasyLibby.Api.Entities.AuthorAggregate;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLibby.Api.Entities.BookAggregate
{
    public class Book : BaseEntity
    {
        [Precision(13, 0)]
        public ulong? ISBN { get; set; }
        [Required]
        [StringLength(255)]
        public required string Title { get; set; }
        public Author? Author { get; set; }

        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }

        public DateTime? PublishedDate { get; set; }
        [Url]
        [StringLength(250)]
        public string? CoverImageUri { get; set; }
    }
}
