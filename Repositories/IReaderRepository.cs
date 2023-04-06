using LibraryAPI.Models;

namespace LibraryAPI.Repositories
{
    public interface IReaderRepository
    {
        Task<IEnumerable<Reader>> GetReaders();
        Task<Reader> GetReaderById(int id);
        Task<IEnumerable<Reader>> GetReadersByName(string name);
        Task AddReader(Reader reader);
        Task UpdateReader(Reader reader);
        Task DeleteReader(int id);
        Task IssueBook(int readerId, int bookId);
        Task ReturnBook(int readerId, int bookId);
    }
}
