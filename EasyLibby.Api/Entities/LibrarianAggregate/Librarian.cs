namespace EasyLibby.Api.Entities.LibrarianAggregate
{
    public class Librarian : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
