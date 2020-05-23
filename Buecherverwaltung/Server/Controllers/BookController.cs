using System.Collections.Generic;
using System.Threading.Tasks;
using Buecherverwaltung.Shared;
using Buecherverwaltung.Server.Application;
using Microsoft.AspNetCore.Mvc;

namespace Buecherverwaltung.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BookDto>>> GetAll()
        {
            var books = await _bookService.Books();
            return Ok(books);
        }

        [HttpGet("{searchText}/{showOnlyLoanedBooks}")]
        public async Task<ActionResult<IReadOnlyList<BookDto>>> Get(string searchText, bool showOnlyLoanedBooks)
        {
            return Ok(await _bookService.Get(searchText, showOnlyLoanedBooks));
        }

        [HttpGet("{showOnlyLoanedBooks}")]
        public async Task<ActionResult<IReadOnlyList<BookDto>>> Get(bool showOnlyLoanedBooks)
        {
            return Ok(await _bookService.Get(showOnlyLoanedBooks));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] BookDto book)
        {
            var added = await _bookService.Add(book);
            return Ok(added);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Put([FromBody] BookDto book)
        {
            var updated = await _bookService.Update(book);
            return Ok(updated);
        }

 
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(string id)
        {
            var deleted = await _bookService.Delete(id);
            return Ok(deleted);
        }
    }
}
