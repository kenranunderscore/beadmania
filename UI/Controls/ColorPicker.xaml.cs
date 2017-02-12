namespace beadmania.UI.Controls
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Logic.Extensions;

    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl, INotifyPropertyChanged
    {
        private static readonly DependencyProperty SelectedColorProperty
            = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(ColorPicker), new PropertyMetadata(SelectedColorChanged));

        public ColorPicker()
        {
            InitializeComponent();
        }

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public byte Red
        {
            get { return SelectedColor.R; }
            set
            {
                SelectedColor = Color.FromArgb(byte.MaxValue, value, SelectedColor.G, SelectedColor.B);
                OnColorsChanged();
            }
        }

        public byte Green
        {
            get { return SelectedColor.G; }
            set
            {
                SelectedColor = Color.FromArgb(byte.MaxValue, SelectedColor.R, value, SelectedColor.B);
                OnColorsChanged();
            }
        }

        public byte Blue
        {
            get { return SelectedColor.B; }
            set
            {
                SelectedColor = Color.FromArgb(byte.MaxValue, SelectedColor.R, SelectedColor.G, value);
                OnColorsChanged();
            }
        }

        public string HexCode => SelectedColor.ToHexCode();

        private void UpdateValues()
        {
            var brush = new SolidColorBrush(SelectedColor);
            brush.Freeze();
            colorArea.Background = brush;
        }

        private void OnColorsChanged()
        {
            OnPropertyChanged(nameof(Red));
            OnPropertyChanged(nameof(Green));
            OnPropertyChanged(nameof(Blue));
            OnPropertyChanged(nameof(HexCode));
        }

        private static void SelectedColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var target = obj as ColorPicker;
            target?.UpdateValues();
            target?.OnColorsChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}