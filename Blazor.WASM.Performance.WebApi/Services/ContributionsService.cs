using Blazor.WASM.Performance.Shared.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Blazor.WASM.Performance.WebApi.Services
{
    public class ContributionsService
    {
        private static Root? _root;

        public Task InitAsync()
        {
            return LoadDataAsync();
        }

        public async Task<List<Contribution>> GetContributionsAsync()
        {
            return _root?.Contributions ?? new List<Contribution>();
        }

        public Contribution? GetContribution(int id, CancellationToken cancellationToken)
        {
            var contribution = _root?.Contributions.FirstOrDefault(c => c.Id == id);
            return contribution;
        }

        private async Task LoadDataAsync()
        {
            var assembly = Assembly.GetEntryAssembly();
            var resourceStream = assembly?.GetManifestResourceStream("Blazor.WASM.Performance.WebApi.Data.contributions.json");
            if (resourceStream != null)
            {
                using var reader = new StreamReader(resourceStream, Encoding.UTF8);
                var jsonString = await reader.ReadToEndAsync();
                _root = JsonSerializer.Deserialize<Root>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    }
}