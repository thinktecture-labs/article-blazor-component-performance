using System.Collections.Generic;

namespace Blazor.WASM.Performance.Shared.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public bool Billed { get; set; }
        public string Abstract { get; set; } = string.Empty;
        public List<Medium> Media { get; set; } = new List<Medium>();
        public string PreviewSrc { get; set; } = string.Empty;
        public List<int> Speaker { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public string PrimaryTag { get; set; } = string.Empty;
        public bool ExternalSpeaker { get; set; }
        public string Time { get; set; } = string.Empty;
        public string UrlFragment { get; set; } = string.Empty;
        public string BigmarkerRef { get; set; } = string.Empty;
        public string CtaKey { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;
        public int Conference { get; set; }
    }
}