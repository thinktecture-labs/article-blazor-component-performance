namespace Blazor.WASM.Shared.Models
{
    public class PersonDto
    {
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public PersonName Name { get; set; }
        public Picture Picture { get; set; }
    }
}