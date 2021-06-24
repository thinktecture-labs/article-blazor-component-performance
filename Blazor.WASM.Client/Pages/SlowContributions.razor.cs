using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Api.Models;
using Blazor.WASM.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Client.Pages
{
    public partial class SlowContributions
    {
        [Inject] public ContributionService ContributionService { get; set; }

        private List<Contribution> _contributions;

        private void Clicked()
        {
            Console.WriteLine("Test");
        }
        
        protected override async Task OnInitializedAsync()
        {
            _contributions = await ContributionService.GetContributionsAsync(0, 1000, CancellationToken.None);
            await base.OnInitializedAsync();
        }
    }
}