using beadmania.Logic.Model;
using beadmania.UI.MVVM;
using System.Collections.Generic;
using System.Windows.Input;

namespace beadmania.UI.ViewModels
{
    internal class PaletteEditorViewModel : ViewModel
    {
        private readonly BeadPalette palette;

        public PaletteEditorViewModel(BeadPalette palette)
        {
            this.palette = palette;
        }

        public ICommand CancelCmd => new RelayCommand(_ => this.DialogResult = true);

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

        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set { SetProperty(ref dialogResult, value); }
        }
    }
}