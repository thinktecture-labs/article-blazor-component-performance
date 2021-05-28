using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Client.Services;
using Blazor.WASM.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazor.WASM.Client.Pages
{
    public partial class Person
    {
        [Parameter] public string Name { get; set; }
        [Inject] public PeopleService PeopleService { get; set; }

        private PersonDto _person;
        private bool _preventRendering = false;
        private EditForm _form;

        protected override async Task OnInitializedAsync()
        {
            _person = await PeopleService.GetPerson(Name, CancellationToken.None);
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
                    if (args.FieldIdentifier.FieldName == nameof(PersonDto.Name))
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

        public void EmailChanged(string email)
        {
            Console.WriteLine(email);
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
    }
}