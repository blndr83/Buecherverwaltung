using Buecherverwaltung.Shared;
using Buecherverwaltung.Client.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Buecherverwaltung.Client.ViewModels
{
    public class EditBookViewModel
    {
        private readonly HttpService _httpService;

        public EditBookViewModel(HttpService httpService)
        {
            _httpService = httpService;
        }
        public BookDto Book { get; set; }

        public Action OnButtonClick { get; set; }

        public async Task Update()
        {
            var json = await _httpService.Put(Book, Constants.BookApiUri);
            var updated = JsonConvert.DeserializeObject<int>(json);
            if(updated > 0) OnButtonClick();
        }
    }
}