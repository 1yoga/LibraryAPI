using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace LibraryAPI.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly ApplicationDbContext _context;

        public ReaderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reader>> GetReaders()
        {
            return await _context.Readers
                .Include(r => r.BookIssues)
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<Reader> GetReaderById(int id)
        {
            var reader = await _context.Readers
                .Include(r => r.BookIssues) 
                .FirstOrDefaultAsync(r => r.Id == id);
            return reader ?? throw new InvalidOperationException($"Reader with ID {id} not found.");
        }

        public async Task<IEnumerable<Reader>> GetReadersByName(string name)
        {
            return await _context.Readers.Where(r => r.FullName.Contains(name)).ToListAsync();
        }

        public async Task AddReader(Reader reader)
        {
            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReader(Reader reader)
        {
            _context.Entry(reader).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReader(int id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
            {
                return;
            }

            reader.IsDeleted = true;
            _context.Entry(reader).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<BookIssue?> IssueBook(int readerId, int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book.AvailableCopies > 0)
            {
                book.AvailableCopies--;

                var bookIssue = new BookIssue
                {
                    ReaderId = readerId,
                    BookId = bookId,
                    IssueDate = DateTime.Now
                };

                _context.BookIssues.Add(bookIssue);

                await _context.SaveChangesAsync();
                return bookIssue;
            }
            else
            {
                return null; 
            }
        }

        public async Task ReturnBook(int readerId, int bookId)
        {
            var bookIssue = await _context.BookIssues
                .FirstOrDefaultAsync(bi => bi.ReaderId == readerId && bi.BookId == bookId && bi.ReturnDate == null);

            if (bookIssue != null)
            {
                var book = await _context.Books.FindAsync(bookId);
                book.AvailableCopies++;

                bookIssue.ReturnDate = DateTime.Now;
                _context.Entry(bookIssue).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
