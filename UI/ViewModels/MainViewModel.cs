namespace beadmania.UI.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Input;
    using beadmania.Logic;
    using beadmania.Logic.Converters;
    using beadmania.Logic.Delta;
    using beadmania.Logic.Model;
    using beadmania.UI.MVVM;
    using beadmania.UI.Services;
    using Logic.IO;
    using Logic.Repositories;

    internal class MainViewModel : ViewModel
    {
        private BeadPattern pattern;
        private bool showGrid = true;
        private string imagePath;
        private BeadPalette selectedPalette;
        private ObservableCollection<BeadPalette> allPalettes = new ObservableCollection<BeadPalette>();

        public MainViewModel(IFileSystemService fileSystemService, IPaletteRepository paletteRepository, IDialogService dialogService)
        {
            FileSystemService = fileSystemService;
            DialogService = dialogService;
            PaletteRepository = paletteRepository;

            AllPalettes = new ObservableCollection<BeadPalette>(PaletteRepository.Load());
            SelectedPalette = AllPalettes.FirstOrDefault();
        }

        private IFileSystemService FileSystemService { get; }

        private IDialogService DialogService { get; }

        private IPaletteRepository PaletteRepository { get; }

        public ObservableCollection<BeadPalette> AllPalettes { get; }

        public BeadPalette SelectedPalette
        {
            get { return selectedPalette; }
            set { SetProperty(ref selectedPalette, value); }
        }

        public BeadPattern Pattern
        {
            get { return pattern; }
            private set { SetProperty(ref pattern, value); }
        }

        public bool ShowGrid
        {
            get { return showGrid; }
            set { SetProperty(ref showGrid, value); }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                SetProperty(ref imagePath, value);
                if (FileSystemService.FileExists(imagePath))
                    LoadBitmap();
            }
        }

        public ICommand NewPaletteCmd => new RelayCommand(_ => DialogService.OpenDialog(new PaletteEditorViewModel(PaletteRepository, null)));

        public ICommand DeletePaletteCmd =>
            new RelayCommand(_ => PaletteRepository.Delete(SelectedPalette), _ => SelectedPalette != null);

        public ICommand EditPaletteCmd => new RelayCommand(_ =>
        {
            var result = DialogService.OpenDialog(new PaletteEditorViewModel(PaletteRepository, SelectedPalette.Clone()));
            if (result == true)
            {

            }
        }, _ => SelectedPalette != null);

        public ICommand OpenImageCmd => new RelayCommand(_ => ImagePath = DialogService.ChooseFile(null, "Image files|*.png;*.jpg;*.bmp"));

        public ICommand LoadPaletteCmd => new RelayCommand(_ =>
        {
            string fileName = DialogService.ChooseFile(null, $"Bead palettes|*.{ConfigConstants.PaletteFileExtension}");
            if (!string.IsNullOrEmpty(fileName))
            {
                BeadPalette palette = PaletteRepository.Load(fileName);
                AllPalettes.Add(palette);
                SelectedPalette = palette;
            }
        });

        public ICommand ConvertCmd => new RelayCommand(
                _ => Pattern = new BeadPatternConverter(SelectedPalette, new DeltaE94Distance()).Convert(Pattern),
                _ => Pattern != null && SelectedPalette != null);

        private void LoadBitmap()
        {
            using (var fileStream = FileSystemService.OpenFile(ImagePath))
            {
                Bitmap image = (Bitmap)Image.FromStream(fileStream);
                Pattern = BeadPattern.FromBitmap(image);
            }
        }
    }
}