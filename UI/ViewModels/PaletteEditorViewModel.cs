namespace beadmania.UI.ViewModels
{
    using System.Windows.Input;
    using beadmania.Logic.Model;
    using beadmania.UI.MVVM;
    using Logic.Repositories;
    using Services;

    internal class PaletteEditorViewModel : DialogViewModel
    {
        private readonly BeadPalette palette;

        public PaletteEditorViewModel(IPaletteRepository paletteRepository, IDialogService dialogService, BeadPalette palette)
        {
            this.palette = palette;
            PaletteRepository = paletteRepository;
            DialogService = dialogService;
        }

        private IPaletteRepository PaletteRepository { get; }

        private IDialogService DialogService { get; }

        public BeadPalette Palette => palette;

        public ICommand EditColorCmd => new RelayCommand(_ => DialogService.PickColor());

        public ICommand SaveCmd => new RelayCommand(_ => Save());

        private void Save()
        {
            DialogResult = true;
        }
    }
}