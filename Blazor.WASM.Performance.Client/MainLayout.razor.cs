using MudBlazor;

namespace Blazor.WASM.Performance.Client
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
            },
            Typography = new Typography()
            {
                Default = new()
                {
                    FontFamily = new string[4] { "Fredoka", "Helvetica", "Arial", "sans-serif" },
                    FontSize = ".875rem",
                    FontWeight = 400,
                    LineHeight = 1.43,
                    LetterSpacing = ".01071em"
                },
                H1 = new() { FontFamily = new string[4] { "Fredoka", "Helvetica", "Arial", "sans-serif" } },
                H2 = new() { FontFamily = new string[4] { "Fredoka", "Helvetica", "Arial", "sans-serif" } },
                H3 = new() { FontFamily = new string[4] { "Fredoka", "Helvetica", "Arial", "sans-serif" } },
                H4 = new() { FontFamily = new string[4] { "Fredoka", "Helvetica", "Arial", "sans-serif" } },
                H5 = new() { FontFamily = new string[4] { "Fredoka", "Helvetica", "Arial", "sans-serif" } },
                H6 = new() { FontFamily = new string[4] { "Fredoka", "Helvetica", "Arial", "sans-serif" } }
            }
        };
        private void ToggleDrawer()
        {
            _open = !_open;
        }
    }
}