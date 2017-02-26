namespace beadmania.Logic.Model
{
    using System.Drawing;
    using Extensions;

    public sealed class BeadPattern
    {
        private readonly Bead[,] beads;

        public BeadPattern(int width, int height)
        {
            Width = width;
            Height = height;
            beads = new Bead[Width, Height];
        }

        public static BeadPattern FromBitmap(Bitmap bitmap)
        {
            BeadPattern pattern = new BeadPattern(bitmap.Width, bitmap.Height);
            for (int i = 0; i < bitmap.Width; ++i)
            {
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    if (color.IsTransparentWhiteOrBlack())
                        color = Color.White;
                    pattern.beads[i, j] = new Bead { Color = color };
                }
            }
            return pattern;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Bead this[int i, int j]
        {
            get { return beads[i, j]; }
            set { beads[i, j] = value; }
        }
    }
}