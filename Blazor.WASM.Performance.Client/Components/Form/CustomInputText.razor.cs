using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Performance.Client.Components.Form
{
    public partial class CustomInputText
    {
        [Parameter] public string Id { get; set; } = string.Empty;
        [Parameter] public string Label { get; set; } = string.Empty;
        [Parameter] public bool PreventRendering { get; set; }

        private string _renderMessage = string.Empty;
        private int _renderCount = 0;
        private int _valueHashCode;

        protected override bool ShouldRender()
        {
            if (PreventRendering)
            {
                var lastHashCode = _valueHashCode;
                _valueHashCode = Value?.GetHashCode() ?? 0;
                return _valueHashCode != lastHashCode;
            }

            return base.ShouldRender();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            _renderCount++;
            _renderMessage = firstRender ? "Rendered by first Render" : "Render by ShouldRender";
            _renderMessage = $"Override ShouldRender Input: {_renderMessage}. Rendered {_renderCount} times.";
            base.OnAfterRender(firstRender);
        }

        protected override bool TryParseValueFromString(string? value, out string result,
            out string validationErrorMessage)
        {
            result = value ?? string.Empty;
            validationErrorMessage = string.Empty;
            return true;
        }
    }
}