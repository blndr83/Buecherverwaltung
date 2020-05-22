using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Buecherverwaltung.Server.OrMapper.Models;
using Buecherverwaltung.Server.Services;
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
        public async Task<ActionResult<IList<Book>>> GetAll()
        {
            var books = await _bookService.Books();
            return Ok(books);
        }

        [HttpGet("{searchText}/{showOnlyLoanedBooks}")]
        public async Task<ActionResult<IList<Book>>> Get(string searchText, bool showOnlyLoanedBooks)
        {
            var searchtext = searchText.ToLower();
            return showOnlyLoanedBooks ? await GetBooksByExpression(_ => _.IsLoaned == showOnlyLoanedBooks &&
                (_.ArticleNumber.ToLower().Contains(searchtext) || _.Title.ToLower().Contains(searchtext))) :
                await GetBooksByExpression(_ => _.ArticleNumber.ToLower().Contains(searchtext) || _.Title.ToLower().Contains(searchtext));
        }

        private async Task<ActionResult<IList<Book>>> GetBooksByExpression(Expression<Func<Book, bool>> searchExpression = null)
        {
            var books = searchExpression == null ? await _bookService.Books() : await _bookService.FindBooks(searchExpression);
            return Ok(books);
        }

        [HttpGet("{showOnlyLoanedBooks}")]
        public async Task<ActionResult<IList<Book>>> Get(bool showOnlyLoanedBooks)
        {

            return showOnlyLoanedBooks ? await GetBooksByExpression(_ => _.IsLoaned == showOnlyLoanedBooks) :  await GetBooksByExpression();
   
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Book book)
        {
            var added = await _bookService.Add(book);
            return Ok(added);
        }


        [HttpPut]
        public async Task<ActionResult<int>> Put([FromBody] Book book)
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
