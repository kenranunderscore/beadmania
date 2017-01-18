using System.IO;

namespace beadmania.UI
{
    public interface IFileSystemService
    {
        string OpenFileDialog(string initialPath);
        FileStream OpenFile(string path);
    }
}