using System.IO;

namespace beadmania.UI
{
    public interface IIOService
    {
        string ChooseFile(string initialPath);
        string ChooseFile(string initialPath, string filter);
        FileStream OpenFile(string path);
        bool FileExists(string path);
    }
}