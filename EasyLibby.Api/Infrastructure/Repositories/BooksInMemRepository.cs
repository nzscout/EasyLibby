using EasyLibby.Api.Data;
using EasyLibby.Api.Entities.BookAggregate;
using System.Linq.Expressions;

namespace EasyLibby.Api.Infrastructure.Repositories;

//depreciated, no longer used in the project
public class BooksInMemRepository : IRepository<Book>
{
    private readonly List<Book> books;

    public BooksInMemRepository()
    {
        books = SeedData.GetBooks().ToList();
    }

    public async Task<IEnumerable<Book>> GetAllAsync(int pageNumber, int pageSize, Func<Book, bool>? filter = null)
    {
        var skipCount = (pageNumber - 1) * pageSize;

        return await Task.FromResult(FilterBooks(filter).Skip(skipCount).Take(pageSize));
    }

    public async Task<Book?> GetAsync(int id)
    {
        return await Task.FromResult(books.Find(Book => Book.Id == id));
    }

    public async Task CreateAsync(Book Book)
    {
        Book.Id = books.Max(Book => Book.Id) + 1;
        books.Add(Book);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Book updatedBook)
    {
        var index = books.FindIndex(Book => Book.Id == updatedBook.Id);
        books[index] = updatedBook;

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = books.FindIndex(Book => Book.Id == id);
        books.RemoveAt(index);

        await Task.CompletedTask;
    }

    public async Task<int> CountAsync(Func<Book, bool>? filter = null)
    {
        return await Task.FromResult(FilterBooks(filter).Count());
    }

    private IEnumerable<Book> FilterBooks(Func<Book, bool>? filter = null)
    {
        if (filter is null)
        {
            return books;
        }

        return books.Where(filter);
    }

    public Task<IEnumerable<Book>> GetAllAsync(int pageNumber, int pageSize, Expression<Func<Book, bool>>? filter = null)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<Book, bool>>? filter = null)
    {
        throw new NotImplementedException();
    }
}