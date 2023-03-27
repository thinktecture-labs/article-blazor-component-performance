using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Performance.Client.Pages
{
    public partial class Index
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToContributions(bool virtualize = true)
        {
            if (virtualize)
            {
                NavigationManager.NavigateTo("/contributions");
            }
            else
            {
                NavigationManager.NavigateTo("/SlowContributions");
            }
        }
    }
}