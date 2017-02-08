namespace beadmania.UI.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using System.Xml.Linq;
    using beadmania.Logic.Converters;
    using beadmania.Logic.Delta;
    using beadmania.Logic.Model;
    using beadmania.UI.MVVM;
    using beadmania.UI.Services;
    using Logic.IO;

    internal class MainViewModel : ViewModel
    {
        private readonly IFileSystemService ioService;
        private readonly IDialogService dialogService;

        private BeadPattern pattern;
        private bool showGrid = true;
        private string imagePath;
        private BeadPalette selectedPalette;
        private ObservableCollection<BeadPalette> allPalettes = new ObservableCollection<BeadPalette>();

        public MainViewModel(IFileSystemService ioService, IDialogService dialogService)
        {
            this.ioService = ioService;
            this.dialogService = dialogService;
            AllPalettes = new ObservableCollection<BeadPalette>(LoadPalettesFromXml());
            SelectedPalette = AllPalettes.FirstOrDefault();
        }

        public ICommand NewPaletteCmd => new RelayCommand(_ => dialogService.OpenDialog(new PaletteEditorViewModel(ioService, null)));

        public ICommand DeletePaletteCmd => new RelayCommand(_ =>
        {
            string paletteFile = ConfigConstants.PaletteFolderName + Path.DirectorySeparatorChar + SelectedPalette.Name + $".{ConfigConstants.PaletteFileExtension}";
            File.Delete(paletteFile);
        }, _ => SelectedPalette != null);

        public ICommand EditPaletteCmd => new RelayCommand(_ =>
        {
            var result = dialogService.OpenDialog(new PaletteEditorViewModel(ioService, SelectedPalette.Clone()));
            if (result == true)
            {

            }
        }, _ => SelectedPalette != null);

        public ICommand OpenImageCmd => new RelayCommand(_ => ImagePath = dialogService.ChooseFile(null, "Image files|*.png;*.jpg;*.bmp"));

        public ICommand LoadPaletteCmd => new RelayCommand(_ =>
        {
            string fileName = dialogService.ChooseFile(null, "Bead palettes|*.bpal");
            if (!string.IsNullOrEmpty(fileName))
            {
                BeadPalette palette = LoadPalette(fileName);
                AllPalettes.Add(palette);
            }
        });

        public ICommand ConvertCmd => new RelayCommand(
                _ => Pattern = new BeadPatternConverter(SelectedPalette, new DeltaE94Distance()).Convert(Pattern),
                _ => Pattern != null && SelectedPalette != null);

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
                if (ioService.FileExists(imagePath))
                    LoadBitmap();
            }
        }

        private void LoadBitmap()
        {
            using (var fileStream = ioService.OpenFile(ImagePath))
            {
                Bitmap image = (Bitmap)Image.FromStream(fileStream);
                Pattern = BeadPattern.FromBitmap(image);
            }
        }

        private IEnumerable<BeadPalette> LoadPalettesFromXml()
        {
            List<BeadPalette> palettes = new List<BeadPalette>();
            foreach (string fileName in ioService.GetFileNamesInCurrentDirectory($"*.{ConfigConstants.PaletteFileExtension}"))
            {
                palettes.Add(LoadPalette(fileName));
            }
            return palettes.OrderBy(x => x.Name);
        }

        private BeadPalette LoadPalette(string fileName)
        {
            using (var fileStream = ioService.OpenFile(fileName))
            {
                var xml = XDocument.Load(fileStream);
                return BeadPalette.FromXml(xml);
            }
        }
    }
}