using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : ControllerBase
    {
        private readonly IReaderService _readerService;

        public ReadersController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        // GET: api/readers
        [HttpGet]
        public async Task<IEnumerable<Reader>> GetReaders()
        {
            return await _readerService.GetReaders();
        }

        // GET: api/readers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reader>> GetReader(int id)
        {
            var reader = await _readerService.GetReaderById(id);
            if (reader == null)
            {
                return NotFound();
            }
            return reader;
        }

        // PUT: api/readers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReader(int id, Reader reader)
        {
            if (id != reader.Id)
            {
                return BadRequest();
            }

            await _readerService.UpdateReader(reader);
            return NoContent();
        }

        // POST: api/readers
        [HttpPost]
        public async Task<ActionResult<Reader>> AddReader(Reader reader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _readerService.AddReader(reader);
            return CreatedAtAction(nameof(GetReader), new { id = reader.Id }, reader);
        }

        // DELETE: api/readers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReader(int id)
        {
            await _readerService.DeleteReader(id);
            return NoContent();
        }

        // GET: api/readers/search?name=readername
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Reader>>> Search(string name)
        {
            var readers = await _readerService.GetReadersByName(name);
            return Ok(readers);
        }

        // POST: api/readers/5/issuebook/2
        [HttpPost("{readerId}/issuebook/{bookId}")]
        public async Task<IActionResult> IssueBook(int readerId, int bookId)
        {
            await _readerService.IssueBook(readerId, bookId);
            return NoContent();
        }

        // POST: api/readers/5/returnbook/2
        [HttpPost("{readerId}/returnbook/{bookId}")]
        public async Task<IActionResult> ReturnBook(int readerId, int bookId)
        {
            await _readerService.ReturnBook(readerId, bookId);
            return NoContent();
        }
    }
}
