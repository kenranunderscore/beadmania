namespace beadmania.Logic.IO
{
    using System.Collections.Generic;
    using System.IO;

    public class FileSystemService : IFileSystemService
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public Stream OpenFile(string path)
        {
            return File.Open(path, FileMode.Open);
        }

        public IEnumerable<string> GetFileNamesInCurrentDirectory(string filter)
        {
            return Directory.EnumerateFiles(".", filter, SearchOption.TopDirectoryOnly);
        }
    }
}