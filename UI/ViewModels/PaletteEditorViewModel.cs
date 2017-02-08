namespace beadmania.UI.ViewModels
{
    using System.IO;
    using System.Windows.Input;
    using beadmania.Logic.Model;
    using beadmania.UI.MVVM;
    using Logic;
    using Logic.IO;

    internal class PaletteEditorViewModel : DialogViewModel
    {
        private readonly BeadPalette palette;

        public PaletteEditorViewModel(IFileSystemService fileSystemService, BeadPalette palette)
        {
            this.palette = palette;
            this.FileSystemService = fileSystemService;
        }

        public ICommand SaveCmd => new RelayCommand(_ => Save());

        private IFileSystemService FileSystemService { get; }

        public BeadPalette Palette => palette;
        
        //TODO: Extract saving logic
        private void Save()
        {
            string fileName = ConfigConstants.PaletteFolderName + Path.DirectorySeparatorChar + palette.Name + $".{ConfigConstants.PaletteFileExtension}";
            var fileContent = palette.ToXml();
            if (!Directory.Exists(ConfigConstants.PaletteFolderName))
                Directory.CreateDirectory(ConfigConstants.PaletteFolderName);

            using (FileStream fs = File.OpenWrite(fileName))
            {
                fileContent.Save(fs);
            }

            DialogResult = true;
        }
    }
}