using System.IO;

namespace beadmania.UI
{
    public interface IIOService
    {
        string OpenFileDialog(string initialPath);
        FileStream OpenFile(string path);
        bool FileExists(string path);
    }
}