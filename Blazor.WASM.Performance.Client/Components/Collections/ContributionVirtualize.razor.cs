using Blazor.WASM.Performance.Client.Services;
using Blazor.WASM.Performance.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Blazor.WASM.Performance.Client.Components.Collections
{
    public partial class ContributionVirtualize
    {
        [Inject] private ContributionService _contributionService { get; set; } = default!;

        private async ValueTask<ItemsProviderResult<Contribution>> LoadContributions(
            ItemsProviderRequest request)
        {
            var count = await _contributionService.GetContributionCountAsync(request.CancellationToken);
            var numContributions = Math.Min(request.Count, count - request.StartIndex);
            var contributions =
                await _contributionService.GetContributionsAsync(request.StartIndex, numContributions,
                    request.CancellationToken);
            return new ItemsProviderResult<Contribution>(contributions, count);
        }
    }
}