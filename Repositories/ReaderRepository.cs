using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Readers.ToListAsync();
        }

        public async Task<Reader> GetReaderById(int id)
        {
            return await _context.Readers.FindAsync(id);
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
            if (reader != null)
            {
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
            }
        }

        public async Task IssueBook(int readerId, int bookId)
        {
            var bookIssue = new BookIssue
            {
                ReaderId = readerId,
                BookId = bookId,
                IssueDate = DateTime.UtcNow
            };

            _context.BookIssues.Add(bookIssue);
            await _context.SaveChangesAsync();
        }

        public async Task ReturnBook(int readerId, int bookId)
        {
            var bookIssue = await _context.BookIssues
                .FirstOrDefaultAsync(bi => bi.ReaderId == readerId && bi.BookId == bookId && bi.ReturnDate == null);

            if (bookIssue != null)
            {
                bookIssue.ReturnDate = DateTime.UtcNow;
                _context.Entry(bookIssue).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
