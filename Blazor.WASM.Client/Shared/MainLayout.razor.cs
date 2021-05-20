using MudBlazor;

namespace Blazor.WASM.Client.Shared
{
    public partial class MainLayout
    {
        private bool _open;
        
        private readonly MudTheme _defaultTheme = new()
        {
            Palette = new Palette()
            {
                Black = "#272c34",
                AppbarBackground = "#ff584f",
                ActionDefault = "#ff584f",
                Primary = "#ff584f",
                Secondary = "#3d6fb4"
            }
        };
        private void ToggleDrawer()
        {
            _open = !_open;
        }
    }
}