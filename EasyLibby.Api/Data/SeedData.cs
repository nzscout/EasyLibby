using EasyLibby.Api.Entities.AuthorAggregate;
using EasyLibby.Api.Entities.BookAggregate;
using EasyLibby.Api.Entities.LibrarianAggregate;
using EasyLibby.Api.Entities.LoansAggregate;
using EasyLibby.Api.Entities.MemberAggregate;

namespace EasyLibby.Api.Data
{
    public static class SeedData
    {
        private static readonly Random _random = new Random();
        public static IEnumerable<Author> GetAuthors() =>
            [
                new()
                {
                    Id = 1,
                    FirstName = "Eckhart",
                    LastName = "Tolle",
                    DateBirth = new  DateTime(1948, 2, 16)
                }
                ,
                new Author()
                {
                    Id = 2,
                    FirstName = "Jordan B.",
                    LastName = "Peterson",
                    DateBirth = new DateTime(1962, 6, 12)
                },
                new Author()
                {
                    Id = 3,
                    FirstName = "Paulo",
                    LastName = "Coelho",
                    DateBirth = new DateTime(1945, 3, 6)
                },
                new Author()
                {
                    Id = 4,
                    FirstName = "Gabriel García",
                    LastName = "Márquez",
                    DateBirth = new DateTime(1927, 3, 6)
                }
            ];


        public static IEnumerable<Book> GetBooks() =>
            [
                new Book()
                {
                    Id = 1,
                    Title = "12 Rules for Life: An Antidote to Chaos",
                    ISBN = 9780345816023,
                    CoverImageUri = "https://images.isbndb.com/covers/60/47/9780345816047.jpg",
                    PublishedDate = new DateTime(1975, 3, 6),
                    AuthorId = 2
                },
                new Book()
                {
                    Id = 2,
                    Title = "The Power of Now: A Guide to Spiritual Enlightenment",
                    ISBN = 9781577311522,
                    CoverImageUri = "https://images.isbndb.com/covers/15/22/9781577311522.jpg",
                    PublishedDate = new DateTime(1985, 3, 6),
                    AuthorId = 1
                },
                new Book()
                {
                    Id = 3,
                    Title = "A New Earth: Awakening to Your Life's Purpose",
                    ISBN = 9780141039411,
                    CoverImageUri = "https://images.isbndb.com/covers/94/11/9780141039411.jpg",
                    PublishedDate = new DateTime(1995, 9, 8),
                    AuthorId = 1
                },
                new Book()
                {
                    Id = 4,
                    Title = "The Alchemist",
                    ISBN = 9780062315007,
                    CoverImageUri = "https://covers.openlibrary.org/b/id/45762-M.jpg",
                    PublishedDate = new DateTime(1989, 2, 1),
                    AuthorId = 3
                },
                new Book()
                {
                    Id = 5,
                    Title = "Eleven Minutes",
                    ISBN = 9780060589288,
                    CoverImageUri = "https://covers.openlibrary.org/b/id/31229-M.jpg",
                    PublishedDate = new DateTime(1997, 1, 4),
                    AuthorId = 3
                },
                new Book()
                {
                    Id = 6,
                    Title = "One Hundred Years of Solitude",
                    ISBN = 9780140157512,
                    CoverImageUri = "https://covers.openlibrary.org/b/id/40565-M.jpg",
                    PublishedDate = new DateTime(1967, 5, 1),
                    AuthorId = 4
                },
                new Book()
                {
                    Id = 7,
                    Title = "The Autumn of the Patriarch",
                    ISBN = 9780330255608,
                    CoverImageUri = "https://covers.openlibrary.org/b/id/10097251-M.jpg",
                    PublishedDate = new DateTime(1978, 5, 31),
                    AuthorId = 4
                }
            ];

        public static IEnumerable<Member> GetMembers()
        {
            int i = 0;
            var members = new List<Member>()
            {
                new Member()
                {
                    Id = ++i,
                    FirstName = "John",
                    LastName = "Doe",
                    DateBirth = new DateTime(1980, 1, 1),
                    Email = "John@gmail2.com",
                    Password = "pass123word"
                },
                new Member()
                {
                    Id = ++i,
                    FirstName = "Jane",
                    LastName = "Doe",
                    DateBirth = new DateTime(1985, 1, 1),
                    Email = "jane@gmail2.com",
                    Password = "pass45word"
                },
                new Member()
                {
                    Id = ++i,
                    FirstName = "Alice",
                    LastName = "Smith",
                    DateBirth = new DateTime(1990, 1, 1),
                    Email = "alice@gmail2.com",
                    Password = "pass67word"
                },
                new Member()
                {
                    Id = ++i,
                    FirstName = "Bob",
                    LastName = "Smith",
                    DateBirth = new DateTime(1995, 1, 1),
                    Email = "bob@gmail2.com",
                    Password = "pass89word"
                }
            };
            return members;
        }

        public static IEnumerable<Loan> GetLoans()
        {
            int i = 0;
            var loans = new List<Loan>()
            {
                new Loan()
                {
                    Id = ++i,
                    BookId = 1,
                    MemberId = 1,
                    BorrowedDate = DateTime.Now.AddDays(-5),
                    DueDate = DateTime.Now.AddDays(30-5),
                    RenewedTimes = 1
                },
                new Loan()
                {
                    Id = ++i,
                    BookId = 3,
                    MemberId = 1,
                    BorrowedDate = DateTime.Now.AddDays(-7),
                    DueDate = DateTime.Now.AddDays(30-7),
                }
            };
            return loans;
        }

        public static IEnumerable<Librarian> GetLibrarians()
        {
            int i = 0;
            var librarians = new List<Librarian>()
            {
                new Librarian()
                {
                    Id = ++i,
                    FirstName = "LibJohn",
                    LastName = "LibDoe",
                    Email = "libJohn@gmail2.com",
                    Password = "pass123word"
                },
                new Librarian()
                {
                    Id = ++i,
                    FirstName = "LibJane",
                    LastName = "LibDoe",
                    Email = "Libjane@gmail2.com",
                    Password = "pass678word"
                }
            };
            return librarians;
        }
    }
}

