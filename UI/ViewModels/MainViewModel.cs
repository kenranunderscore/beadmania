using beadmania.UI.MVVM;
using System.Drawing;
using System.Windows.Input;

namespace beadmania.UI.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private readonly IIOService ioService;

        private Bitmap bitmap;
        private bool showGrid = true;
        private string imagePath;

        public MainViewModel(IIOService ioService)
        {
            this.ioService = ioService;
            OpenImageCmd = new RelayCommand(_ => ImagePath = this.ioService.ChooseFile(null, "Image files|*.png;*.jpg;*.bmp"));
        }

        public ICommand OpenImageCmd { get; }

        public Bitmap Bitmap
        {
            get { return bitmap; }
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
                bitmap = (Bitmap)Image.FromStream(fileStream);
                OnPropertyChanged(nameof(Bitmap));
            }
        }
    }
}