using Buecherverwaltung.Server.OrMapper.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Buecherverwaltung.Server.Services
{
    public class BookService : ServiceBase<Book>, IBookService
    {
        public BookService(DbContext context) : base(context)
        {

        }

        public async Task<IList<Book>> Books()
        {
            return await GetAll();
        }


        public async Task<int> Add(Book newBook)
        {
            if (!string.IsNullOrWhiteSpace(newBook.ArticleNumber) && !string.IsNullOrWhiteSpace(newBook.Title))
            { 
                newBook.ArticleNumber = newBook.ArticleNumber.Trim();
                newBook.Title = newBook.Title.Trim();
                if (!Find(b => b.ArticleNumber.ToLower().Equals(newBook.ArticleNumber.ToLower())
                  || b.Title.ToLower().Equals(newBook.Title.ToLower())).Any())
                {
                    return await Insert(newBook);
                }
            }
            return await Task.FromResult(0);

        }

        public async Task<int> Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var book = GetById(id);
                if (book != null) return await Remove(book);
            }
            return await Task.FromResult(0);
        }

        public async Task<IList<Book>> FindBooks(Expression<Func<Book, bool>> expression)
        {
            return await Find(expression).ToListAsync();
        }

        public async Task<int> Update(Book bookToUpdate)
        {
            var book = GetById(bookToUpdate.ArticleNumber);
            if (book != null)
            {
                return await Update(bookToUpdate, book);
            }
            return await Task.FromResult(0);
        }
    }
}
