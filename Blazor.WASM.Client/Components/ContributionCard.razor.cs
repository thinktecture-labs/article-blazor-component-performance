using System;
using Blazor.WASM.Api.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Client.Components
{
    public partial class ContributionCard
    {
        [Parameter] public Contribution Contribution { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine($"After render called on Contribution {Contribution.Id}");
            base.OnAfterRender(firstRender);
        }

        private void NavigateToConference()
        {
            NavigationManager.NavigateTo($"/contributions/{Contribution.Id}");
        }
    }
}