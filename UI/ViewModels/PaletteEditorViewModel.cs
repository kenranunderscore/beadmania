namespace beadmania.UI.ViewModels
{
    using System.Windows.Input;
    using beadmania.Logic.Model;
    using beadmania.UI.MVVM;
    using Logic.Repositories;

    internal class PaletteEditorViewModel : DialogViewModel
    {
        private readonly BeadPalette palette;

        public PaletteEditorViewModel(IPaletteRepository paletteRepository, BeadPalette palette)
        {
            this.palette = palette;
            PaletteRepository = paletteRepository;
        }

        public ICommand SaveCmd => new RelayCommand(_ => Save());

        private IPaletteRepository PaletteRepository { get; }

        public BeadPalette Palette => palette;

        private void Save()
        {
            PaletteRepository.Save(palette);
            DialogResult = true;
        }
    }
}