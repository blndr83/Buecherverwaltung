using Buecherverwaltung.Server.OrMapper.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Buecherverwaltung.Server.Services
{
    public interface IBookService
    {
        Task<IList<Book>> Books();
        Task<int> Add(Book newBook);
        Task<int> Delete(string id);
        Task<int> Update(Book bookToUpdate);
        Task<IList<Book>> FindBooks(Expression<Func<Book, bool>> expression);
    }
}
