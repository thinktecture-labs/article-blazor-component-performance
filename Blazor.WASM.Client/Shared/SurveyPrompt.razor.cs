using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Client.Shared
{
    public partial class SurveyPrompt
    {
        // Demonstrates how a parent component can supply parameters
        [Parameter]
        public string Title { get; set; }
    }
}