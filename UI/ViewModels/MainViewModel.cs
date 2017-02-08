namespace beadmania.UI.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using System.Xml.Linq;
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
            AllPalettes = new ObservableCollection<BeadPalette>(LoadPalettesFromXml());
            SelectedPalette = AllPalettes.FirstOrDefault();
        }

        public ICommand NewPaletteCmd => new RelayCommand(_ => DialogService.OpenDialog(new PaletteEditorViewModel(FileSystemService, null)));

        public ICommand DeletePaletteCmd => new RelayCommand(_ =>
        {
            string paletteFile = ConfigConstants.PaletteFolderName + Path.DirectorySeparatorChar + SelectedPalette.Name + $".{ConfigConstants.PaletteFileExtension}";
            File.Delete(paletteFile);
        }, _ => SelectedPalette != null);

        public ICommand EditPaletteCmd => new RelayCommand(_ =>
        {
            var result = DialogService.OpenDialog(new PaletteEditorViewModel(FileSystemService, SelectedPalette.Clone()));
            if (result == true)
            {

            }
        }, _ => SelectedPalette != null);

        public ICommand OpenImageCmd => new RelayCommand(_ => ImagePath = DialogService.ChooseFile(null, "Image files|*.png;*.jpg;*.bmp"));

        public ICommand LoadPaletteCmd => new RelayCommand(_ =>
        {
            string fileName = DialogService.ChooseFile(null, "Bead palettes|*.bpal");
            if (!string.IsNullOrEmpty(fileName))
            {
                BeadPalette palette = PaletteRepository.Load(fileName);
                AllPalettes.Add(palette);
            }
        });

        public ICommand ConvertCmd => new RelayCommand(
                _ => Pattern = new BeadPatternConverter(SelectedPalette, new DeltaE94Distance()).Convert(Pattern),
                _ => Pattern != null && SelectedPalette != null);

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

        private void LoadBitmap()
        {
            using (var fileStream = FileSystemService.OpenFile(ImagePath))
            {
                Bitmap image = (Bitmap)Image.FromStream(fileStream);
                Pattern = BeadPattern.FromBitmap(image);
            }
        }

        private IEnumerable<BeadPalette> LoadPalettesFromXml()
        {
            List<BeadPalette> palettes = new List<BeadPalette>();
            foreach (string fileName in FileSystemService.GetFileNamesInCurrentDirectory($"*.{ConfigConstants.PaletteFileExtension}"))
            {
                palettes.Add(PaletteRepository.Load(fileName));
            }
            return palettes.OrderBy(x => x.Name);
        }
    }
}