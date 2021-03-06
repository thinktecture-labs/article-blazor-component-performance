using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Api.Models;

namespace Blazor.WASM.Client.Services
{
    public class ContributionService
    {
        private readonly HttpClient _httpClient;

        public ContributionService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<int> GetContributionCountAsync(
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, 
                    $"https://localhost:5001/contributions"),
                    cancellationToken);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response.Headers.GetValues("X-Contribution-Count"));
                if (response.Headers.TryGetValues("Contribution-Count", out var values) &&
                    int.TryParse(values.First(), out var count))
                {
                    return count;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Load Contributions failed. Exception {e.Message}");
            }

            // Fallback value for sample. Do NOT USE in Production!
            return 198;
        }

        public async Task<List<Contribution>> GetContributionsAsync(int skip, int take,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Contribution>>(
                    $"https://localhost:5001/contributions?skip={skip}&take={take}",
                    cancellationToken);
                return response ?? new List<Contribution>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Load Contributions failed. Exception {e.Message}");
                return new List<Contribution>();
            }
        }

        public async Task<Contribution> GetContribution(int id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Contribution>(
                    $"https://localhost:5001/contributions/{id}",
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