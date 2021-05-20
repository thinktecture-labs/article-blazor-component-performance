using Blazor.WASM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Client.Components
{
    public partial class PersonItem
    {
        [Parameter] public PersonDto Person { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private void NavigateToPerson()
        {
            NavigationManager.NavigateTo($"/people/{Person.Name.First}-{Person.Name.Last}");
        }
    }
}