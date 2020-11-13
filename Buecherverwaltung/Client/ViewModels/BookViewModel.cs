using Buecherverwaltung.Shared;
using Buecherverwaltung.Client.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Buecherverwaltung.Client.ViewModels
{
    public class BookViewModel
    {
        private readonly HttpService _httpService;

        public BookViewModel(HttpService httpService)
        {
            _httpService = httpService;
        }

        public IList<BookDto> Books { get; set; }
        public async Task InitBooks()
        {
            var json = await _httpService.Get(Constants.BookApiUri);
            Books = JsonConvert.DeserializeObject<IList<BookDto>>(json);
        }        

        public string Titel => "Books";

        [Required]
        public string BookArticleNumber { get; set; }

        [Required]
        public string BookTitle { get; set; }

        public bool CanNotAdd => string.IsNullOrWhiteSpace(BookArticleNumber) || string.IsNullOrWhiteSpace(BookTitle);

        public string SearchText { get; set; }

        public bool ShowOnlyLoanedBooks { get; set; }

        public async Task Add()
        {
            var newBook = new BookDto { ArticleNumber = BookArticleNumber, Title = BookTitle, IsLoaned = false };
            var json = await _httpService.Post(newBook, Constants.BookApiUri);
            var added = JsonConvert.DeserializeObject<int>(json);
            if (added > 0)
            {
                BookArticleNumber = string.Empty;
                BookTitle = string.Empty;
                if(!ShowOnlyLoanedBooks) Books.Add(newBook);
            }

        }

        public async Task UpdateIsLoaned(bool isLoaned, string articleNumber)
        {
            var book = Books.First(_ => _.ArticleNumber.Equals(articleNumber));
            book.IsLoaned = isLoaned;
            var json = await _httpService.Put(book, Constants.BookApiUri);
            var updated = JsonConvert.DeserializeObject<int>(json);
            if (updated > 0 && ShowOnlyLoanedBooks && !isLoaned) Books.Remove(book);
        }

        public async Task Delete(string articleNumber)
        {
            var json = await _httpService.Delete(articleNumber, Constants.BookApiUri);
            var deleted = JsonConvert.DeserializeObject<int>(json);
            if(deleted > 0)
            {
                var book = Books.First(_ => _.ArticleNumber.Equals(articleNumber));
                Books.Remove(book);
            }

        }

        public async Task UpateShowOnlyLoanedBooks(bool showOnlyLoanedBooks )
        {
            ShowOnlyLoanedBooks = showOnlyLoanedBooks;
            await UpdateDisplayedBooks();
        }

        public async Task UpdateDisplayedBooks()
        {
            var showOnlyLoanedBooks = JsonConvert.ToString(ShowOnlyLoanedBooks);
            var parameters = string.IsNullOrEmpty(SearchText) ? showOnlyLoanedBooks : $"{SearchText}/{showOnlyLoanedBooks}";
            var json = await _httpService.Get(Constants.BookApiUri, parameters);
            Books = JsonConvert.DeserializeObject<IList<BookDto>>(json);
        }

    }
}
