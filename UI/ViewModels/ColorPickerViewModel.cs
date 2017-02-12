namespace beadmania.UI.ViewModels
{
    using System.Windows.Input;
    using System.Windows.Media;
    using beadmania.UI.MVVM;

    internal class ColorPickerViewModel : DialogViewModel
    {
        private Color selectedColor;

        public Color SelectedColor
        {
            get { return selectedColor; }
            set { SetProperty(ref selectedColor, value); }
        }

        public ICommand OkCmd => new RelayCommand(_ => DialogResult = true);
    }
}