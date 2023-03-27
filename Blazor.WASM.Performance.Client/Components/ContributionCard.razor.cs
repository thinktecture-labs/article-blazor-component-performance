using Microsoft.AspNetCore.Components;
using Blazor.WASM.Performance.Shared.Models;

namespace Blazor.WASM.Performance.Client.Components
{
    public partial class ContributionCard
    {
        [Parameter] public Contribution Contribution { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

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