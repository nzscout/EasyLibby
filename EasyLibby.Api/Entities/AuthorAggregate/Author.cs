using System.ComponentModel.DataAnnotations;

namespace EasyLibby.Api.Entities.AuthorAggregate
{
    public class Author : BaseEntity
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        public string GetFullName() => $"{FirstName} {LastName}".Trim();
        public DateTime? DateBirth { get; set; }
    }
}
