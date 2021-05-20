using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Client.Pages
{
    public partial class Index
    {
        [Inject] public NavigationManager NavigationManager { get; set; }

        private void NavigateToPeople(bool virtualize = true)
        {
            if (virtualize)
            {
                NavigationManager.NavigateTo("/people");
            }
            else
            {
                NavigationManager.NavigateTo("/SlowPeople");
            }
        }
    }
}