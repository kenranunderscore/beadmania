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

        public string ChooseFile(string initialPath)
        {
            return ChooseFile(initialPath, null);
        }

        public string ChooseFile(string initialPath, string filter)
        {
            var openFileDialog = new OpenFileDialog { InitialDirectory = initialPath, Filter = filter };
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return null;
        }
    }
}