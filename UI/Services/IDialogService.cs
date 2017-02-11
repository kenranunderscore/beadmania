namespace beadmania.UI.Services
{
    using System.Windows.Media;
    using beadmania.UI.MVVM;

    public interface IDialogService
    {
        bool? OpenDialog(ViewModel vm);
        string ChooseFile(string initialPath);
        string ChooseFile(string initialPath, string filter);
        Color? PickColor(Color? initialColor);
    }
}