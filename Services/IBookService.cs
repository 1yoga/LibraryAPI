using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<IEnumerable<Book>> GetBooksByName(string name);
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
        Task<IEnumerable<Book>> GetIssuedBooks();
        Task<IEnumerable<Book>> GetAvailableBooks();
    }
}
