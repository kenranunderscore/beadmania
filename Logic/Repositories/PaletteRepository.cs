namespace beadmania.Logic.Repositories
{
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
    }
}