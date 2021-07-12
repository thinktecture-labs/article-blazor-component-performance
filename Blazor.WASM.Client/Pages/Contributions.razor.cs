using System;
using System.Threading.Tasks;
using Blazor.WASM.Api.Models;
using Blazor.WASM.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Blazor.WASM.Client.Pages
{
    public partial class Contributions
    {
        [Inject] public ContributionService ContributionService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async ValueTask<ItemsProviderResult<Contribution>> LoadContributions(
            ItemsProviderRequest request)
        {
            //TODO: Replace count with API Data
            var count = await ContributionService.GetContributionCountAsync(request.CancellationToken);
            
            var numContributions = Math.Min(request.Count, count - request.StartIndex);
            var contributions =
                await ContributionService.GetContributionsAsync(request.StartIndex, numContributions,
                    request.CancellationToken);
            return new ItemsProviderResult<Contribution>(contributions, count);
        }


        private void Clicked()
        {
            Console.WriteLine("Test");
        }
    }
}