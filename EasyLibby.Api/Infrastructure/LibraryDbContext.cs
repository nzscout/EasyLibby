using EasyLibby.Api.Entities.AuthorAggregate;
using EasyLibby.Api.Entities.BookAggregate;
using EasyLibby.Api.Entities.LibrarianAggregate;
using EasyLibby.Api.Entities.LoansAggregate;
using EasyLibby.Api.Entities.MemberAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EasyLibby.Api.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                //.AreUnicode(false)
                .HaveMaxLength(512);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
