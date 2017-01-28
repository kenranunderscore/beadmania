using System.Drawing;

namespace beadmania.Logic.Model
{
    public sealed class BeadPattern
    {
        private Bead[,] beads;

        private BeadPattern() { }

        public static BeadPattern FromBitmap(Bitmap bitmap)
        {
            BeadPattern pattern = new BeadPattern
            {
                Width = bitmap.Width,
                Height = bitmap.Height,
                beads = new Bead[bitmap.Width, bitmap.Height]
            };
            for (int i = 0; i < bitmap.Width; ++i)
            {
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    pattern.beads[i, j] = new Bead { Color = color };
                }
            }
            return pattern;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Bead this[int i, int j] => beads[i, j];
    }
}