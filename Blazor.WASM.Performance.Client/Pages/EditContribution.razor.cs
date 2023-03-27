using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blazor.WASM.Performance.Client.Services;
using Blazor.WASM.Performance.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;

namespace Blazor.WASM.Performance.Client.Pages
{
    public partial class EditContribution
    {
        [Parameter] public int Id { get; set; }
        [Inject] public ContributionService ContributionService { get; set; } = default!;

        private Contribution? _contribution;
        private bool _preventRendering = false;
        private EditForm? _form;

        protected override async Task OnInitializedAsync()
        {
            _contribution = await ContributionService.GetContribution(Id, CancellationToken.None);
            await base.OnInitializedAsync();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (_form?.EditContext != null)
            {
                _form.EditContext.OnFieldChanged += (sender, args) =>
                {
                    var senderString = JsonSerializer.Serialize(sender);
                    if (sender != null)
                    {
                        Console.WriteLine(sender.GetType().FullName);
                    }

                    Console.WriteLine(senderString);
                    if (args.FieldIdentifier.FieldName == nameof(Contribution.Title))
                    {
                    }
                };
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        private void TogglePreventRendering()
        {
            _preventRendering = !_preventRendering;
            StateHasChanged();
        }
        private void TriggerRendering()
        {
            if (_preventRendering)
            {
                Console.WriteLine("Trigger re-rendering with prevent Rendering");
            }
            else
            {
                Console.WriteLine("Trigger re-rendering without prevent Rendering");
            }
        }

        private string GetPowerIcon()
        {
            return _preventRendering ? Icons.Material.Filled.Power : Icons.Material.Filled.PowerOff;
        }
    }
}