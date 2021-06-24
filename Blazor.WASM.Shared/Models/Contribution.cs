using System.Collections.Generic;

namespace Blazor.WASM.Api.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Language { get; set; }
        public bool Billed { get; set; }
        public string Abstract { get; set; }
        public List<Medium> Media { get; set; }
        public string PreviewSrc { get; set; }
        public List<int> Speaker { get; set; }
        public List<string> Tags { get; set; }
        public string PrimaryTag { get; set; }
        public bool ExternalSpeaker { get; set; }
        public string Time { get; set; }
        public string UrlFragment { get; set; }
        public string BigmarkerRef { get; set; }
        public string CtaKey { get; set; }
        public string AdditionalInfo { get; set; }
        public int Conference { get; set; }
    }
}