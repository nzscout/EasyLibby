using System.ComponentModel.DataAnnotations;

namespace EasyLibby.Api.Entities.MemberAggregate
{
    public class Member : BaseEntity
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        public DateTime DateBirth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
