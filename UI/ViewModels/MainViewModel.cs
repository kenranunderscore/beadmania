using beadmania.UI.General;
using System.Drawing;
using System.Windows.Input;

namespace beadmania.UI.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private readonly IIOService fileSystemService;

        private Bitmap bitmap;
        private bool showGrid = true;
        private string imagePath;

        public MainViewModel(IIOService fileSystemService)
        {
            this.fileSystemService = fileSystemService;
            OpenImageCmd = new RelayCommand(_ => ImagePath = this.fileSystemService.OpenFileDialog(null));
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
                if (fileSystemService.FileExists(imagePath))
                    LoadBitmap();
            }
        }

        private void LoadBitmap()
        {
            using (var fileStream = fileSystemService.OpenFile(ImagePath))
            {
                bitmap = (Bitmap)Image.FromStream(fileStream);
                OnPropertyChanged(nameof(Bitmap));
            }
        }
    }
}