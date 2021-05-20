namespace Blazor.WASM.Client.Models
{
    public class PhoneData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public PhoneType PhoneType { get; set; }
    }

    public enum PhoneType
    {
        iPhone,
        Samsung,
        Other
    }
}