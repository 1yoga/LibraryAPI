using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookService.GetBooks();
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // PUT: api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _bookService.UpdateBook(book);
            return NoContent();
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _bookService.AddBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return NoContent();
        }

        // GET: api/books/search?name=bookname
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> Search(string name)
        {
            var books = await _bookService.GetBooksByName(name);
            return Ok(books);
        }

        // GET: api/books/issued
        [HttpGet("issued")]
        public async Task<ActionResult<IEnumerable<Book>>> GetIssuedBooks()
        {
            var books = await _bookService.GetIssuedBooks();
            return Ok(books);
        }

        // GET: api/books/available
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAvailableBooks()
        {
            var books = await _bookService.GetAvailableBooks();
            return Ok(books);
        }
    }
}
