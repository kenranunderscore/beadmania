namespace beadmania.UI.Views
{
    using System.Windows;
    using System.Windows.Media;
    using beadmania.Logic.Extensions;

    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : Window
    {
        private Color? selectedColor;

        public ColorPicker()
            : this(null)
        {
        }

        public ColorPicker(Color? initialColor)
        {
            InitializeComponent();
            SelectedColor = initialColor;
        }

        public Color? SelectedColor
        {
            get { return selectedColor; }
            private set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    txtHexCode.Text = selectedColor?.ToHexCode();
                    txtRed.Text = selectedColor?.R.ToString("d3");
                    txtGreen.Text = selectedColor?.G.ToString("d3");
                    txtBlue.Text = selectedColor?.B.ToString("d3");
                    Brush brush = new SolidColorBrush(selectedColor ?? Colors.Transparent);
                    colorArea.Background = brush;
                }
            }
        }
    }
}