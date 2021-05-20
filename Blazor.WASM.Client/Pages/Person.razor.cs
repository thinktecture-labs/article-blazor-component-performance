using System;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Client.Services;
using Blazor.WASM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Client.Pages
{
    public partial class Person
    {
        [Parameter] public string Name { get; set; }
        [Inject] public PeopleService PeopleService { get; set; }

        private PersonDto _person;
        private bool _preventRendering = false;

        protected override async Task OnInitializedAsync()
        {
            _person = await PeopleService.GetPerson(Name, CancellationToken.None);
            await base.OnInitializedAsync();
        }

        private void TogglePreventRendering()
        {
            _preventRendering = !_preventRendering;
            StateHasChanged();
        }

        private void TriggerRendering()
        {
            if (_preventRendering)
            {
                Console.WriteLine("Trigger re-rendering with prevent Rendering");
            }
            else
            {
                Console.WriteLine("Trigger re-rendering without prevent Rendering");
            }
        }
    }
}