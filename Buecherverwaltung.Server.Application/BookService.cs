using AutoMapper;
using Buecherverwaltung.Server.Core;
using Buecherverwaltung.Server.Core.Entities;
using Buecherverwaltung.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Buecherverwaltung.Server.Application
{
    internal class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public BookService(IRepository repository, IMapper mapper) 
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> Add(BookDto book)
        {
            return await _repository.Add(_mapper.Map<Book>(book));
        }

        public async Task<IReadOnlyList<BookDto>> Books()
        {
            return _mapper.Map<IReadOnlyList<BookDto>>(await _repository.GetAll<Book>());
        }

        public async Task<int> Delete(string id)
        {
            return await _repository.Delete(await _repository.Find<Book>(id));
        }

        private async Task<IReadOnlyList<BookDto>> GetBooksByExpression(Expression<Func<Book, bool>> searchExpression = null)
        {
            var books = searchExpression == null ? await Books() : _mapper.Map<IReadOnlyList<BookDto>>(await _repository.FindAll<Book>(searchExpression));
            return books;
        }

        public async Task<IReadOnlyList<BookDto>> Get(string searchText, bool showOnlyLoanedBooks)
        {
           var searchtext = searchText.ToLower();
            return showOnlyLoanedBooks ? await GetBooksByExpression(_ => _.IsLoaned == showOnlyLoanedBooks &&
                (_.ArticleNumber.ToLower().Contains(searchtext) || _.Title.ToLower().Contains(searchtext))) :
                await GetBooksByExpression(_ => _.ArticleNumber.ToLower().Contains(searchtext) || _.Title.ToLower().Contains(searchtext));
        }

        public async Task<IReadOnlyList<BookDto>> Get(bool showOnlyLoanedBooks)
        {
             return showOnlyLoanedBooks ? await GetBooksByExpression(_ => _.IsLoaned == showOnlyLoanedBooks) :  await GetBooksByExpression();
        }

        public async Task<int> Update(BookDto book)
        {
           var bookEntity = await _repository.Find<Book>(book.ArticleNumber);
            _ = _mapper.Map(book, bookEntity);
            return await _repository.Update<Book>(bookEntity);
        }
    }
}
