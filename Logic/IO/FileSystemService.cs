namespace beadmania.Logic.IO
{
    using System.Collections.Generic;
    using System.IO;

    internal class FileSystemService : IFileSystemService
    {
        public bool FileExists(string path) => File.Exists(path);

        public Stream OpenFile(string path) => File.OpenRead(path);

        public Stream OpenWrite(string path) => File.OpenWrite(path);

        public IEnumerable<string> FileNamesInFolder(string path, string filter) =>
            Directory.EnumerateFiles(path, filter, SearchOption.TopDirectoryOnly);
    }
}