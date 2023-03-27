using Microsoft.AspNetCore.Components;
using Blazor.WASM.Performance.Client.Services;
using Blazor.WASM.Performance.Shared.Models;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.QuickGrid;
using static MudBlazor.CategoryTypes;

namespace Blazor.WASM.Performance.Client.Pages
{
    public partial class Contributions
    {
        [Inject] private ContributionService _contributionService { get; set; } = default!;
        [Parameter] public string Mode { get; set; } = "fast";

        private GridItemsProvider<Contribution>? _contributionsProvider;
        private bool _showGridView = false;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
        protected override async Task OnInitializedAsync()
        {
            _contributionsProvider = async req =>
            {
                var count = await _contributionService.GetContributionCountAsync(req.CancellationToken);
                var response = await _contributionService.GetContributionsAsync(req.StartIndex, req.Count ?? 100, req.CancellationToken);
                return GridItemsProviderResult.From(
                    items: response ?? new(),
                    totalItemCount: count);
            };

            await base.OnInitializedAsync();
        }

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

        private async Task GoToPageAsync(int pageIndex)
        {
            await pagination.SetCurrentPageIndexAsync(pageIndex);
        }

        private string? PageButtonClass(int pageIndex)
            => pagination.CurrentPageIndex == pageIndex ? "current" : null;

        private string? AriaCurrentValue(int pageIndex)
            => pagination.CurrentPageIndex == pageIndex ? "page" : null;

        private void Clicked()
        {
            Console.WriteLine("Test");
        }
    }
}