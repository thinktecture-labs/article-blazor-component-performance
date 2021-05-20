using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Shared.Models;

namespace Blazor.WASM.Api.Services
{
    public class PeopleService
    {
        private readonly HttpClient _httpClient;
        private static List<PersonDto> _people = new();

        public PeopleService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<PersonDto>> GetPeopleAsync(int skip = 0, int take = 100,
            CancellationToken cancellationToken = default)
        {
            if (_people.Count <= 0)
            {
                await LoadPeopleIntoCache(cancellationToken);
            }
            else
            {
                await Task.Delay(200, cancellationToken);
            }

            return _people.Skip(skip).Take(take).ToList();
        }

        public async Task<PersonDto> GetPersonAsync(string name, CancellationToken cancellationToken = default)
        {
            if (_people.Count <= 0)
            {
                await LoadPeopleIntoCache(cancellationToken);
            }

            var person = _people?.FirstOrDefault(p => $"{p.Name.First}-{p.Name.Last}" == name);
            return person;
        }

        private async Task LoadPeopleIntoCache(CancellationToken cancellationToken)
        {
            var apiResult = await _httpClient.GetFromJsonAsync<PeopleApiResult>(
                $"https://randomuser.me/api/?results=1000",
                cancellationToken);
            if (apiResult != null)
            {
                _people = apiResult.Results.ToList();
            }
        }
    }
}