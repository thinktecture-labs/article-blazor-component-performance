using System.Collections.Generic;

namespace Blazor.WASM.Performance.Shared.Models
{
    public class Root
    {
        public List<Contribution> Contributions { get; set; } = new();
        public List<Speaker> Speaker { get; set; } = new();
        public int ItemCount { get; set; }
        public List<Conference> Conferences { get; set; } = new();
    }
}