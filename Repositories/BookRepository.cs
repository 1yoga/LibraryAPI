using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books
                .Include(b => b.BookIssues)
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookIssues) 
                .FirstOrDefaultAsync(b => b.Id == id);

            return book ?? throw new InvalidOperationException($"Book with ID {id} not found.");
        }

        public async Task<IEnumerable<Book>> GetBooksByName(string name)
        {
            return await _context.Books
                .Include(b => b.BookIssues)
                .ThenInclude(bookIssue => bookIssue.Reader)
                .Where(b => EF.Functions.ILike(b.Name, $"%{name}%") && !b.IsDeleted)
                .ToListAsync();

            return await _context.Books.Where(b => b.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAvailableBooks()
        {
            return await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetIssuedBooks()
        {
            return await _context.BookIssues
                .Where(bi => bi.ReturnDate == null)
                .Select(bi => bi.Book)
                .ToListAsync();
        }

        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return;
            }

            book.IsDeleted = true;
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
