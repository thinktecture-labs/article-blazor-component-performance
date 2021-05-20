using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.WASM.Client.Components
{
    public partial class DebounceTextArea : IDisposable
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Label { get; set; }

        [Parameter] public bool PreventRendering { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

        private ElementReference _textareaElement;
        private DotNetObjectReference<DebounceTextArea> _selfReference;
        private string _renderMessage;
        private int _renderCount = 0;
        private int _valueHashCode;

        [JSInvokable]
        public void HandleOnInput(string value)
        {
            Console.WriteLine($"TextChanged {Value}. JS Value {value}");
            if (Value != value)
            {
                StateHasChanged();
                _renderCount++;
            }
        }

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

        protected override bool TryParseValueFromString(string value, out string result,
            out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Console.WriteLine("Register debounce JS Event");
                _selfReference = DotNetObjectReference.Create(this);
                var minInterval = 500; // Only notify every 500 ms
                await JS.InvokeVoidAsync("onDebounceInput",
                    _textareaElement, _selfReference, minInterval);
            }

            _renderCount++;
            _renderMessage = firstRender ? "Rendered by first Render" : "Render by ShouldRender";
            _renderMessage = $"Override ShouldRender Input: {_renderMessage}. Rendered {_renderCount} times.";
            Console.WriteLine("Debounced TextArea: After Render called.");
        }

        public void Dispose() => _selfReference?.Dispose();
    }
}