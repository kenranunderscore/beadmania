using System.Drawing;

namespace beadmania.Logic.Model
{
    public sealed class BeadPattern
    {
        private readonly Bead[,] beads;

        public BeadPattern(Bitmap bitmap)
        {
            Width = bitmap.Width;
            Height = bitmap.Height;
            beads = new Bead[Width, Height];
            for (int i = 0; i < bitmap.Width; ++i)
            {
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    beads[i, j] = new Bead { Color = color };
                }
            }
        }

        public int Width { get; }

        public int Height { get; }

        public Bead this[int i, int j] => beads[i, j];
    }
}