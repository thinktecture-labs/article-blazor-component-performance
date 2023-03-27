using Blazor.WASM.Performance.Client.Services;
using Blazor.WASM.Performance.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Performance.Client.Components.Collections
{
    public partial class ContributionsLoop
    {
        [Inject] private ContributionService _contributionService { get; set; } = default!;

        private IEnumerable<Contribution> _contributions = Enumerable.Empty<Contribution>();
        private bool _isLoading = false;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                _isLoading = true;
                _contributions = await _contributionService.GetContributionsAsync(0, int.MaxValue, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally { _isLoading = false; }
            await base.OnInitializedAsync();
        }
    }
}