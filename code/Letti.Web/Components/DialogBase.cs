using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.Web.Components
{
    public abstract class DialogBase : ComponentBase
    {
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        public bool ShowDialog { get; set; }
        public abstract Task Show();

        protected abstract void ResetDialog();
        protected abstract Task HandleValidSubmit();

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }
    }
}
