namespace beadmania.UI.Services
{
    using System.Windows;
    using beadmania.UI.MVVM;

    internal class DialogService : IDialogService
    {
        public bool? OpenDialog(ViewModel vm)
        {
            Window w = new Window { Content = vm, SizeToContent = SizeToContent.WidthAndHeight };
            return w.ShowDialog();
        }
    }
}