using beadmania.UI.General;
using System.Drawing;

namespace beadmania.UI.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private Bitmap bitmap = (Bitmap)Image.FromFile(@"d:\soh.png");
        public Bitmap Bitmap
        {
            get { return bitmap; }
            set { SetProperty(ref bitmap, value); }
        }

        private bool showGrid = true;
        public bool ShowGrid
        {
            get { return showGrid; }
            set { SetProperty(ref showGrid, value); }
        }
    }
}