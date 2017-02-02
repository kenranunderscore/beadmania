using beadmania.Logic.Model;
using beadmania.UI.MVVM;

namespace beadmania.UI.ViewModels
{
    internal class PaletteEditorViewModel : ViewModel
    {
        private readonly BeadPalette palette;

        public PaletteEditorViewModel(BeadPalette palette)
        {
            this.palette = palette;
        }
    }
}