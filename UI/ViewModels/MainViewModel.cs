using beadmania.Logic.Delta;
using beadmania.Logic.Model;
using beadmania.UI.MVVM;
using beadmania.UI.Services;
using System.Drawing;
using System.Windows.Input;

namespace beadmania.UI.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private readonly IIOService ioService;

        private BeadPattern pattern;
        private bool showGrid = true;
        private string imagePath;

        public MainViewModel(IIOService ioService)
        {
            this.ioService = ioService;
            Palette = new BeadPalette();
            Palette.Add(new Bead { Description = "Black", Color = Color.Black });
            Palette.Add(new Bead { Description = "White", Color = Color.White });
            Palette.Add(new Bead { Description = "Gray", Color = Color.Gray });
            Palette.Add(new Bead { Description = "LightGray", Color = Color.LightGray });
            Palette.Add(new Bead { Description = "DarkGray", Color = Color.DarkGray });
            OpenImageCmd = new RelayCommand(_ => ImagePath = this.ioService.ChooseFile(null, "Image files|*.png;*.jpg;*.bmp"));
            ConvertCmd = new RelayCommand(_ => Pattern = Pattern.Convert(Palette, new DeltaE94Distance()), _ => Pattern != null);
        }

        public ICommand OpenImageCmd { get; }

        public ICommand ConvertCmd { get; }

        public BeadPalette Palette { get; set; }

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
    }
}