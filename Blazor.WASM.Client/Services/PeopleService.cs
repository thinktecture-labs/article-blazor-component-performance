using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Shared.Models;

namespace Blazor.WASM.Client.Services
{
    public class PeopleService
    {
        private readonly HttpClient _httpClient;

        public PeopleService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<PersonDto>> GetPeopleAsync(int skip, int take, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<PersonDto[]>(
                    $"https://localhost:5001/people?skip={skip}&take={take}",
                    cancellationToken);
                return response != null ? response.ToList() : new List<PersonDto>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Load People failed. Exception {e.Message}");
                return new List<PersonDto>();
            }
        }

        public async Task<PersonDto> GetPerson(string name, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<PersonDto>(
                    $"https://localhost:5001/people/{name}",
                    cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Person not found. Exception {e.Message}");
                return null;
            }
        }
    }
}