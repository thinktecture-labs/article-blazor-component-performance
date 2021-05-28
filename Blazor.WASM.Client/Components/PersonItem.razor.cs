using System;
using Blazor.WASM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Client.Components
{
    public partial class PersonItem
    {
        [Parameter] public PersonDto Person { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine($"After render called on Person {Person.Name.First} {Person.Name.Last}");
            base.OnAfterRender(firstRender);
        }

        private void NavigateToPerson()
        {
            NavigationManager.NavigateTo($"/people/{Person.Name.First}-{Person.Name.Last}");
        }
    }
}