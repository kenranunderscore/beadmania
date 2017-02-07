namespace beadmania.UI.Services
{
    using beadmania.UI.MVVM;

    public interface IDialogService
    {
        bool? OpenDialog(ViewModel vm);
    }
}