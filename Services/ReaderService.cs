using LibraryAPI.Models;
using LibraryAPI.Repositories;

namespace LibraryAPI.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepository;

        public ReaderService(IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public async Task<IEnumerable<Reader>> GetReaders()
        {
            return await _readerRepository.GetReaders();
        }

        public async Task<Reader> GetReaderById(int id)
        {
            return await _readerRepository.GetReaderById(id);
        }

        public async Task<IEnumerable<Reader>> GetReadersByName(string name)
        {
            return await _readerRepository.GetReadersByName(name);
        }

        public async Task AddReader(Reader reader)
        {
            reader.DateOfBirth = reader.DateOfBirth.Date.ToUniversalTime();
            await _readerRepository.AddReader(reader);
        }

        public async Task UpdateReader(Reader reader)
        {
            await _readerRepository.UpdateReader(reader);
        }

        public async Task DeleteReader(int id)
        {
            await _readerRepository.DeleteReader(id);
        }

        public async Task IssueBook(int readerId, int bookId)
        {
            var result = await _readerRepository.IssueBook(readerId, bookId);

            if (result == null)
            {
                throw new InvalidOperationException("All copies of the book are already issued.");
            }
        }

        public async Task ReturnBook(int readerId, int bookId)
        {
            await _readerRepository.ReturnBook(readerId, bookId);
        }
    }
}
