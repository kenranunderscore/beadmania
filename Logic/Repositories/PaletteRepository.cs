namespace beadmania.Logic.Repositories
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;
    using beadmania.Logic.IO;
    using Model;

    internal class PaletteRepository : IPaletteRepository
    {
        public PaletteRepository(IFileSystemService fileSystemService)
        {
            FileSystemService = fileSystemService;
        }

        private IFileSystemService FileSystemService { get; }

        public void Save(BeadPalette palette)
        {
            string fileName = DetermineFullFileNameFromPaletteName(palette.Name);
            var fileContent = palette.ToXml();
            using (Stream stream = FileSystemService.OpenWrite(fileName))
            {
                fileContent.Save(stream);
            }
        }

        public BeadPalette Load(string fileName)
        {
            using (Stream stream = FileSystemService.OpenFile(fileName))
            {
                var xml = XDocument.Load(stream);
                return BeadPalette.FromXml(xml);
            }
        }

        public IEnumerable<BeadPalette> Load()
        {
            var palettes = new List<BeadPalette>();
            foreach (string fileName in FileSystemService.FileNamesInFolder(
                ConfigConstants.PaletteFolderName,
                $"*.{ConfigConstants.PaletteFileExtension}"))
            {
                var palette = Load(fileName);
                palettes.Add(palette);
            }
            return palettes;
        }

        public void Delete(BeadPalette palette)
        {
            string fileName = DetermineFullFileNameFromPaletteName(palette.Name);
            File.Delete(fileName);
        }

        private static string DetermineFullFileNameFromPaletteName(string paletteName) =>
            ConfigConstants.PaletteFolderName + Path.DirectorySeparatorChar + paletteName + $".{ConfigConstants.PaletteFileExtension}";

        private static void CreatePaletteFolderIfNecessary()
        {
            if (!Directory.Exists(ConfigConstants.PaletteFolderName))
                Directory.CreateDirectory(ConfigConstants.PaletteFolderName);
        }
    }
}