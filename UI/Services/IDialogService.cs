namespace beadmania.UI.Services
{
    using beadmania.UI.MVVM;

    public interface IDialogService
    {
        bool? OpenDialog(ViewModel vm);
        string ChooseFile(string initialPath);
        string ChooseFile(string initialPath, string filter);
        void PickColor();
    }
}