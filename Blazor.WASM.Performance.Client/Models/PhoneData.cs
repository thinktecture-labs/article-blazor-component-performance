namespace Blazor.WASM.Performance.Client.Models
{
    public class PhoneData
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public PhoneType PhoneType { get; set; }
    }

    public enum PhoneType
    {
        iPhone,
        Samsung,
        Other
    }
}