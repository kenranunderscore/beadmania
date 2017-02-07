namespace beadmania.UI.Services
{
    using System.Collections.Generic;
    using System.IO;

    public interface IIOService
    {
        string ChooseFile(string initialPath);
        string ChooseFile(string initialPath, string filter);
        IEnumerable<string> GetFileNamesInCurrentDirectory(string filter);
        Stream OpenFile(string path);
        bool FileExists(string path);
    }
}