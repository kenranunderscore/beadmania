namespace beadmania.UI.Services
{
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Win32;
    using ViewModels;

    internal class DialogService : IDialogService
    {
        public bool? OpenDialog(object vm)
        {
            Window w = new Window
            {
                Content = vm,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            return w.ShowDialog();
        }

        public string ChooseFile(string initialPath)
        {
            return ChooseFile(initialPath, null);
        }

        public string ChooseFile(string initialPath, string filter)
        {
            var openFileDialog = new OpenFileDialog { InitialDirectory = initialPath, Filter = filter };
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return null;
        }

        public Color? PickColor(Color? initialColor)
        {
            ColorDialogViewModel vm = new ColorDialogViewModel { SelectedColor = initialColor ?? Colors.Black };
            return OpenDialog(vm) == true ? vm.SelectedColor : initialColor;
        }
    }
}