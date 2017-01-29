using beadmania.Logic.ColorVectors;
using beadmania.Logic.Delta;
using System.Drawing;
using System.Linq;

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

        public BeadPattern Convert(BeadPalette palette)
        {
            BeadPattern convertedPattern = new BeadPattern();
            for (int i = 0; i < Width; ++i)
            {
                for (int j = 0; j < Height; ++j)
                {
                    Color color = beads[i, j].Color;
                    RgbVector rgb = new RgbVector(color);
                    Color bestFit = FindBestFittingColor(rgb, palette, new EuclideanDistance());
                }
            }
            return convertedPattern;
        }

        private static Color FindBestFittingColor(RgbVector rgb, BeadPalette palette, IColorDistance<RgbVector> colorDistance)
        {
            double minimumDistance = double.MaxValue;
            Color bestFit = Color.Empty;

            foreach (var color in palette.Beads.Select(b => b.Color))
            {
                var v = new RgbVector(color);
                double d = colorDistance.Between(rgb, v);
                if (d < minimumDistance)
                {
                    minimumDistance = d;
                    bestFit = color;
                }
            }

            return bestFit;
        }
    }
}