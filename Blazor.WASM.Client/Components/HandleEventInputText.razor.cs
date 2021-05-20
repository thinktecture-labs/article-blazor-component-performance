using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazor.WASM.Client.Components
{
    public partial class HandleEventInputText : IHandleEvent
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public bool PreventRendering { get; set; }

        private bool _suppressRender;
        private string _renderMessage;
        private int _renderCount = 0;

        public Task HandleEventAsync(EventCallbackWorkItem item, object arg)
        {
            try
            {
                var task = item.InvokeAsync(arg);
                var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
                                      task.Status != TaskStatus.Canceled;

                if (!_suppressRender)
                {
                    StateHasChanged();
                }

                return shouldAwaitTask
                    ? CallStateHasChangedOnCompletionAsync(task, _suppressRender)
                    : Task.CompletedTask;
            }
            finally
            {
                _suppressRender = false;
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

        private void SuppressRender()
        {
            if (PreventRendering)
            {
                _suppressRender = true;
            }
        }

        protected override bool TryParseValueFromString(string value, out string result,
            out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}