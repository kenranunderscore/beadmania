namespace beadmania.Logic.Repositories
{
    using System.Collections.Generic;
    using beadmania.Logic.Model;

    public interface IPaletteRepository
    {
        BeadPalette Load(string fileName);
        IEnumerable<BeadPalette> Load();
        void Delete(BeadPalette palette);
        void Save(BeadPalette palette);
    }
}