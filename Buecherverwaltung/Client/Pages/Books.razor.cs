using Buecherverwaltung.Shared;
using Buecherverwaltung.Client.Services;
using Buecherverwaltung.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Buecherverwaltung.Client.Pages
{
    public class BooksBase : ComponentBase
    {
        [Inject]
        HttpService HttpService { get; set; }
        protected EditBookViewModel editBookViewModel;
        protected bool showEditTitleDialog;
        protected BookViewModel bookViewModel;
        protected EditContext editContext;

        protected async override Task OnInitializedAsync()
        {
            bookViewModel = new BookViewModel(HttpService);
            editContext = new EditContext(bookViewModel);
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

        protected async Task OnSubmit()
        {
            if (editContext.Validate())
            {
                await bookViewModel.Add();
            }
        }
    }
}
