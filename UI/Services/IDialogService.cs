namespace beadmania.UI.Services
{
    using System.Windows.Media;

    public interface IDialogService
    {
        bool? OpenDialog(object vm);

        string ChooseFile(string initialPath);

        string ChooseFile(string initialPath, string filter);

        Color PickColor(Color initialColor);
    }
}