using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Client.Services;
using Blazor.WASM.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Blazor.WASM.Client.Pages
{
    public partial class SlowPeople
    {
        [Inject] public PeopleService PeopleService { get; set; }

        private List<PersonDto> _people;

        protected override async Task OnInitializedAsync()
        {
            _people = await PeopleService.GetPeopleAsync(0, 1000, CancellationToken.None);
            await base.OnInitializedAsync();
        }
    }
}