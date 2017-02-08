namespace beadmania.Logic.IO
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFileSystemService
    {
        IEnumerable<string> GetFileNamesInCurrentDirectory(string filter);
        Stream OpenFile(string path);
        bool FileExists(string path);
    }
}