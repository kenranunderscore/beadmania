using Microsoft.Win32;
using System.IO;

namespace beadmania.UI
{
    internal class IOService : IIOService
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public FileStream OpenFile(string path)
        {
            return File.Open(path, FileMode.Open);
        }

        public string OpenFileDialog(string initialPath)
        {
            var openFileDialog = new OpenFileDialog { InitialDirectory = initialPath };
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return null;
        }
    }
}