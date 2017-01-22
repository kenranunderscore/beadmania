using beadmania.UI.MVVM;
using System.Windows;

namespace beadmania.UI.Services
{
    internal class DialogService : IDialogService
    {
        public bool? OpenDialog(ViewModel vm)
        {
            Window w = new Window { Content = vm };
            return w.ShowDialog();
        }
    }
}