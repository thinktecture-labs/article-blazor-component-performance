using Blazor.WASM.Performance.Shared.Models;
using System.Net.Http.Json;

namespace Blazor.WASM.Performance.Client.Services
{
    public class ConferencesService
    {
        private readonly HttpClient _httpClient;

        public ConferencesService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<Conference>> GetConferencesAsync(int skip, int take, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Conference>>(
                    $"https://localhost:5001/conferences?skip={skip}&take={take}",
                    cancellationToken);
                return response ?? new List<Conference>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Load Conferences failed. Exception {e.Message}");
                return new List<Conference>();
            }
        }

        public async Task<Conference?> GetConference(int id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Conference>(
                    $"https://localhost:5001/conferences/{id}",
                    cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Conference not found. Exception {e.Message}");
                return null;
            }
        }
    }
}