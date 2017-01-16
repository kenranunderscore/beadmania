using beadmania.UI.General;
using System.Drawing;

namespace beadmania.UI.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            Bitmap = (Bitmap)Image.FromFile(@"d:\soh.png");
        }

        private Bitmap bitmap;
        public Bitmap Bitmap
        {
            get { return bitmap; }
            set { SetProperty(ref bitmap, value); }
        }
    }
}