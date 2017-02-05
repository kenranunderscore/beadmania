using beadmania.Logic.Model;
using beadmania.UI.MVVM;
using beadmania.UI.Services;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace beadmania.UI.ViewModels
{
    internal class PaletteEditorViewModel : DialogViewModel
    {
        private const string TargetFolderName = "Palettes";
        private readonly BeadPalette palette;
        private readonly IIOService ioService;

        public PaletteEditorViewModel(IIOService ioService, BeadPalette palette)
        {
            this.palette = palette;
            this.ioService = ioService;
        }

        public ICommand SaveCmd => new RelayCommand(_ => Save());

        public IEnumerable<Bead> Beads => palette.Beads;

        public string Name => palette.Name;

        //TODO: Extract saving logic
        private void Save()
        {
            string fileName = TargetFolderName + Path.DirectorySeparatorChar + Name;
            var fileContent = palette.ToXml();
            if (!Directory.Exists(TargetFolderName))
                Directory.CreateDirectory(TargetFolderName);

            using (FileStream fs = File.OpenWrite(fileName))
            {
                fileContent.Save(fs);
            }

            DialogResult = true;
        }
    }
}