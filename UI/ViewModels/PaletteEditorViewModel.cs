namespace beadmania.UI.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Windows.Media;
    using beadmania.Logic.Model;
    using beadmania.UI.MVVM;
    using Logic.Extensions;
    using Logic.Repositories;
    using Services;

    internal class PaletteEditorViewModel : DialogViewModel
    {
        private readonly BeadPalette palette;
        private ObservableCollection<Bead> beads;

        public PaletteEditorViewModel(IPaletteRepository paletteRepository, IDialogService dialogService, BeadPalette palette)
        {
            this.palette = palette;
            beads = new ObservableCollection<Bead>(this.palette.Beads);
            PaletteRepository = paletteRepository;
            DialogService = dialogService;
        }

        private IPaletteRepository PaletteRepository { get; }

        private IDialogService DialogService { get; }

        public BeadPalette Palette => palette;

        public ObservableCollection<Bead> Beads => beads;

        public ICommand EditColorCmd => new RelayCommand(p =>
        {
            Bead bead = (Bead)p;
            var pickedColor = DialogService.PickColor(bead.Color.ToMediaColor());
            bead.Color = pickedColor.ToDrawingColor();
        });

        public ICommand AddColorCmd => new RelayCommand(_ =>
        {
            Bead bead = new Bead();
            var pickedColor = DialogService.PickColor(Colors.Black);
            bead.Color = pickedColor.ToDrawingColor();
            if (Palette.Add(bead))
                Beads.Add(bead);
        });

        public ICommand DeleteColorCmd => new RelayCommand(p =>
        {
            Bead bead = (Bead)p;
            Palette.Remove(bead);
            Beads.Remove(bead);
        });

        public ICommand SaveCmd => new RelayCommand(_ => DialogResult = true, _ => !string.IsNullOrWhiteSpace(Palette.Name));
    }
}