using Buecherverwaltung.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace Buecherverwaltung.Client.Dialogs
{
    public class EditBookTitleDialogBase: ComponentBase
    {
        protected EditContext editContext;
        private string _previousTitle;

        protected override void OnInitialized()
        {
            _previousTitle = ViewModel.Book.Title;
            editContext = new EditContext(ViewModel.Book);
            base.OnInitialized();
        }

        [Parameter]
        public EditBookViewModel ViewModel { get; set; }

        protected void onCancelClick()
        {
            ViewModel.Book.Title = _previousTitle;
            ViewModel.OnButtonClick();
        }

        protected async Task OnSubmit()
        {
            if(editContext.Validate())
            {
               await ViewModel.Update();
            }
        }
    }
}
