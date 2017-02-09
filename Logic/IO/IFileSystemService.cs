namespace beadmania.Logic.IO
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFileSystemService
    {
        IEnumerable<string> FileNamesInFolder(string path, string filter);
        Stream OpenFile(string path);
        Stream OpenWrite(string path);
        bool FileExists(string path);
    }
}