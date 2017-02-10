namespace beadmania.UI.Services
{
    using System.Windows;
    using beadmania.UI.MVVM;
    using Microsoft.Win32;
    using Views;

    internal class DialogService : IDialogService
    {
        public bool? OpenDialog(ViewModel vm)
        {
            Window w = new Window { Content = vm, SizeToContent = SizeToContent.WidthAndHeight };
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

        public void PickColor()
        {
            ColorEditor colorEditor = new ColorEditor();
            colorEditor.ShowDialog();
        }
    }
}