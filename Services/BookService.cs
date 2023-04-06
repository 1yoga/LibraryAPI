using LibraryAPI.Models;
using LibraryAPI.Repositories;

namespace LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.GetBooks();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepository.GetBookById(id);
        }

        public async Task<IEnumerable<Book>> GetBooksByName(string name)
        {
            return await _bookRepository.GetBooksByName(name);
        }

        public async Task AddBook(Book book)
        {
            book.AvailableCopies = book.TotalCopies;
            await _bookRepository.AddBook(book);
        }

        public async Task UpdateBook(Book book)
        {
            await _bookRepository.UpdateBook(book);
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteBook(id);
        }

        public async Task<IEnumerable<Book>> GetIssuedBooks()
        {
            return await _bookRepository.GetIssuedBooks();
        }

        public async Task<IEnumerable<Book>> GetAvailableBooks()
        {
            return await _bookRepository.GetAvailableBooks();
        }
    }
}
