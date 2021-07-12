using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Api.Models;

namespace Blazor.WASM.Api.Services
{
    public class ContributionsService
    {
        private static Root _root;

        public Task InitAsync()
        {
            return LoadDataAsync();
        }

        public async Task<List<Contribution>> GetContributionsAsync()
        {
            await Task.Delay(100);
            return _root.Contributions;
        }

        public async Task<Contribution> GetContributionAsync(int id, CancellationToken cancellationToken)
        {
            var contribution = _root?.Contributions.FirstOrDefault(c => c.Id == id);
            return contribution;
        }

        private async Task LoadDataAsync()
        {
            var assembly = Assembly.GetEntryAssembly();
            var resourceStream = assembly?.GetManifestResourceStream("Blazor.WASM.Api.Data.contributions.json");
            using var reader = new StreamReader(resourceStream, Encoding.UTF8);
            var jsonString = await reader.ReadToEndAsync();
            _root = JsonSerializer.Deserialize<Root>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}