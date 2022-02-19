using Letti.Model;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Letti.Web.Components
{
    public class ContractViewBase : DialogBase
    {
        
        public Contract Contract { get; set; }

        public override async Task Show()
        {
            ResetDialog();
            
            ShowDialog = true;
            StateHasChanged();
        }

        protected override async Task HandleValidSubmit()
        {
            ShowDialog = false;
           
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        protected override void ResetDialog()
        {
           
        }
    }
}
