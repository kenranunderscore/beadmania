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
            OpenImageCmd = new RelayCommand(_ => ImagePath = this.ioService.ChooseFile(null, "Image files|*.png;*.jpg;*.bmp"));
        }

        public ICommand OpenImageCmd { get; }

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
                Pattern = new BeadPattern(image);
            }
        }
    }
}