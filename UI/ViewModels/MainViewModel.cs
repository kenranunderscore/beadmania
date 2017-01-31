using beadmania.Logic.Delta;
using beadmania.Logic.Model;
using beadmania.UI.MVVM;
using beadmania.UI.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

namespace beadmania.UI.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private readonly IIOService ioService;

        private BeadPattern pattern;
        private bool showGrid = true;
        private string imagePath;
        private BeadPalette selectedPalette;
        private ObservableCollection<BeadPalette> allPalettes = new ObservableCollection<BeadPalette>();

        public MainViewModel(IIOService ioService)
        {
            this.ioService = ioService;
            OpenImageCmd = new RelayCommand(_ => ImagePath = this.ioService.ChooseFile(null, "Image files|*.png;*.jpg;*.bmp"));
            ConvertCmd = new RelayCommand(_ => Pattern = Pattern.Convert(SelectedPalette, new DeltaE94Distance()), _ => Pattern != null && SelectedPalette != null);
            AllPalettes = new ObservableCollection<BeadPalette>(LoadPalettesFromXml());
            SelectedPalette = AllPalettes.FirstOrDefault();
        }

        public ICommand OpenImageCmd { get; }

        public ICommand ConvertCmd { get; }

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
            foreach (string fileName in ioService.GetFileNamesInCurrentDirectory("*.bpal"))
            {
                using (var fileStream = ioService.OpenFile(fileName))
                {
                    var xml = XDocument.Load(fileStream);
                    var palette = BeadPalette.FromXml(xml);
                    palettes.Add(palette);
                }
            }
            return palettes.OrderBy(x => x.Name);
        }
    }
}