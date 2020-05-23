using Buecherverwaltung.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buecherverwaltung.Server.Application
{
    public interface IBookService
    {
        Task<IReadOnlyList<BookDto>> Books();
        Task<IReadOnlyList<BookDto>> Get(string searchText, bool showOnlyLoanedBooks);
        Task<IReadOnlyList<BookDto>> Get(bool showOnlyLoanedBooks);
        Task<int> Add(BookDto book);
        Task<int> Update(BookDto book);
        Task<int> Delete(string id);
    }
}
