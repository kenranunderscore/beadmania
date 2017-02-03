using beadmania.Logic.Model;
using beadmania.UI.MVVM;
using System.Collections.Generic;

namespace beadmania.UI.ViewModels
{
    internal class PaletteEditorViewModel : ViewModel
    {
        private readonly BeadPalette palette;

        public PaletteEditorViewModel(BeadPalette palette)
        {
            this.palette = palette;
        }

        public IEnumerable<Bead> Beads => palette.Beads;

        public string Name
        {
            get { return palette.Name; }
            set
            {
                palette.Name = value;
                OnPropertyChanged();
            }
        }
    }
}