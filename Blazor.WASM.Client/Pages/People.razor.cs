using System;
using System.Threading.Tasks;
using Blazor.WASM.Client.Services;
using Blazor.WASM.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Blazor.WASM.Client.Pages
{
    public partial class People
    {
        [Inject] public PeopleService PeopleService { get; set; }

        private async ValueTask<ItemsProviderResult<PersonDto>> LoadPeople(
            ItemsProviderRequest request)
        {
            var numEmployees = Math.Min(request.Count, 1000 - request.StartIndex);
            var people =
                await PeopleService.GetPeopleAsync(request.StartIndex, numEmployees, request.CancellationToken);
            return new ItemsProviderResult<PersonDto>(people, 1000);
        }
        
    }
}