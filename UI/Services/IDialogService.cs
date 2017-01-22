using beadmania.UI.MVVM;

namespace beadmania.UI.Services
{
    public interface IDialogService
    {
        bool? OpenDialog(ViewModel vm);
    }
}