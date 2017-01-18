using System;
using System.IO;
using Microsoft.Win32;
using System.Drawing;

namespace beadmania.UI
{
    internal class FileSystemService : IFileSystemService
    {
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