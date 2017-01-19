using beadmania.UI.General;
using beadmania.UI.ViewModels;
using System.Windows;

namespace beadmania.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window
    {
        public MainWindow(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}