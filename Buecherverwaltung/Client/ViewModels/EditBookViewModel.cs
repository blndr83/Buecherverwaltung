using Buecherverwaltung.Client.Models;
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
        public Book Book { get; set; }

        public bool CanNotUpdate => string.IsNullOrWhiteSpace(Book.Title);

        public Action OnButtonClick { get; set; }

        public async Task Update()
        {
            var json = await _httpService.Put(Book, Constants.Book);
            var updated = JsonConvert.DeserializeObject<int>(json);
            if(updated > 0) OnButtonClick();
        }
    }
}