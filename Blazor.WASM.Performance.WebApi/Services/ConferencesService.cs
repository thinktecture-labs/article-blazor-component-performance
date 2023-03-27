using Blazor.WASM.Performance.Shared.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Blazor.WASM.Performance.WebApi.Services
{
    public class ConferencesService
    {
        private static Root? _root;

        public Task InitAsync()
        {
            return LoadDataAsync();
        }

        public async Task<List<Conference>> GetConferencesAsync()
        {
            await Task.Delay(250);
            return _root?.Conferences ?? new List<Conference>();
        }

        public Conference? GetConference(int id, CancellationToken cancellationToken)
        {
            var conf = _root?.Conferences.FirstOrDefault(c => c.Id == id);
            return conf;
        }

        private async Task LoadDataAsync()
        {
            var assembly = Assembly.GetEntryAssembly();
            var resourceStream = assembly?.GetManifestResourceStream("Blazor.WASM.Performance.WebApi.Data.conferences.json");
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