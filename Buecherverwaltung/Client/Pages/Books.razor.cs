using Buecherverwaltung.Shared;
using Buecherverwaltung.Client.Services;
using Buecherverwaltung.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Buecherverwaltung.Client.Pages
{
    public class BooksBase : ComponentBase
    {
        [Inject]
        HttpService HttpService { get; set; }
        protected EditBookViewModel editBookViewModel;
        protected bool showEditTitleDialog;
        protected BookViewModel bookViewModel;

        protected async override Task OnInitializedAsync()
        {
            bookViewModel = new BookViewModel(HttpService);
            await bookViewModel.InitBooks();
        }

        protected void ShowEditTileDialog(BookDto book)
        {
            editBookViewModel = new EditBookViewModel(HttpService)
            {
                Book = book,
                OnButtonClick = OnEditTitleDialogButtonClick
            };
            showEditTitleDialog = true;
        }

        protected void OnEditTitleDialogButtonClick()
        {
            editBookViewModel = null;
            showEditTitleDialog = false;
            StateHasChanged();
        }
    }
}
