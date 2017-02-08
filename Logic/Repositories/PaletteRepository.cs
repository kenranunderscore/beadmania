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
            foreach (string fileName in FileSystemService.GetFileNamesInCurrentDirectory($".{ConfigConstants.PaletteFileExtension}"))
            {
                var palette = Load(fileName);
                palettes.Add(palette);
            }
            return palettes;
        }
    }
}