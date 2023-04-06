using LibraryAPI.Models;

namespace LibraryAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<IEnumerable<Book>> GetBooksByName(string name);
        Task<IEnumerable<Book>> GetAvailableBooks();
        Task<IEnumerable<Book>> GetIssuedBooks();
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
    }
}
