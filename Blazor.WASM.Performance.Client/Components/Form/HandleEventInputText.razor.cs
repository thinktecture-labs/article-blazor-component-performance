using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Performance.Client.Components.Form
{
    public partial class HandleEventInputText : IHandleEvent
    {
        [Parameter] public string Id { get; set; } = string.Empty;
        [Parameter] public string Label { get; set; } = string.Empty;
        [Parameter] public bool PreventRendering { get; set; }

        private bool _preventRender;
        private string _renderMessage = string.Empty;
        private int _renderCount = 0;

        public Task HandleEventAsync(EventCallbackWorkItem item, object? arg)
        {
            try
            {
                var task = item.InvokeAsync(arg);
                var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
                                      task.Status != TaskStatus.Canceled;

                if (!_preventRender)
                {
                    StateHasChanged();
                }

                return shouldAwaitTask
                    ? CallStateHasChangedOnCompletionAsync(task, _preventRender)
                    : Task.CompletedTask;
            }
            finally
            {
                _preventRender = false;
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            _renderCount++;
            _renderMessage = firstRender ? "Rendered by first Render" : "Render by ShouldRender";
            _renderMessage = $"{_renderMessage}. Rendered {_renderCount} times.";
            Console.WriteLine(_renderMessage);
            base.OnAfterRender(firstRender);
        }

        private async Task CallStateHasChangedOnCompletionAsync(Task task, bool supressRender)
        {
            try
            {
                await task;
            }
            catch
            {
                if (task.IsCanceled)
                {
                    return;
                }

                throw;
            }

            if (!supressRender)
            {
                StateHasChanged();
            }
        }

        private void PreventRender()
        {
            if (PreventRendering)
            {
                _preventRender = true;
            }
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