namespace beadmania.UI.ViewModels
{
    using System.IO;
    using System.Windows.Input;
    using beadmania.Logic.Model;
    using beadmania.UI.MVVM;
    using beadmania.UI.Services;

    internal class PaletteEditorViewModel : DialogViewModel
    {
        private readonly BeadPalette palette;
        private readonly IIOService ioService;

        public PaletteEditorViewModel(IIOService ioService, BeadPalette palette)
        {
            this.palette = palette;
            this.ioService = ioService;
        }

        public ICommand SaveCmd => new RelayCommand(_ => Save());

        public BeadPalette Palette => palette;
        
        //TODO: Extract saving logic
        //TODO: Make file extension a constant
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