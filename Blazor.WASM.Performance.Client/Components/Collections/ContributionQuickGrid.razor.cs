using Blazor.WASM.Performance.Client.Services;
using Blazor.WASM.Performance.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Blazor.WASM.Performance.Client.Components.Collections
{
    public partial class ContributionQuickGrid
    {
        [Inject] private ContributionService _contributionService { get; set; } = default!;
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;

        private GridItemsProvider<Contribution>? _contributionsProvider;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 100 };

        protected override async Task OnInitializedAsync()
        {
            _contributionsProvider = async req =>
            {
                // var count = await _contributionService.GetContributionCountAsync(req.CancellationToken);
                var response = await _contributionService.GetContributionsAsync(req.StartIndex, req.Count ?? 100, req.CancellationToken);
                return GridItemsProviderResult.From(
                    items: response ?? new(),
                    totalItemCount: 198);
            };
            pagination.TotalItemCountChanged += (sender, eventArgs) => StateHasChanged();

            await base.OnInitializedAsync();
        }

        private void NavigateToConference(Contribution contribution)
        {
            _navigationManager.NavigateTo($"/contributions/{contribution.Id}");
        }

        private async Task GoToPageAsync(int pageIndex) =>
            await pagination.SetCurrentPageIndexAsync(pageIndex);

        private string? PageButtonClass(int pageIndex)
            => pagination.CurrentPageIndex == pageIndex ? "current" : null;

        private string? AriaCurrentValue(int pageIndex)
            => pagination.CurrentPageIndex == pageIndex ? "page" : null;

    }
}