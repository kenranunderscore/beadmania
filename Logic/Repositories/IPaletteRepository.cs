namespace beadmania.Logic.Repositories
{
    using beadmania.Logic.Model;

    public interface IPaletteRepository
    {
        BeadPalette Load(string fileName);
    }
}