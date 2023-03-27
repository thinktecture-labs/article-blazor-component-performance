using System;

namespace Blazor.WASM.Performance.Shared.Models
{
    public class Conference
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int ContributionsCount { get; set; }
        public int SpeakerCount { get; set; }
        public DateTime? CfpStart { get; set; }
        public DateTime? CfpDeadline { get; set; }
    }
}