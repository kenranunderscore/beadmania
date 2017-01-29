using beadmania.Logic.ColorVectors;
using beadmania.Logic.Delta;
using System.Drawing;
using System.Linq;

namespace beadmania.Logic.Model
{
    public sealed class BeadPattern
    {
        private readonly Bead[,] beads;

        private BeadPattern(int width, int height)
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
                    pattern.beads[i, j] = new Bead { Color = color };
                }
            }
            return pattern;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Bead this[int i, int j] => beads[i, j];

        public BeadPattern Convert(BeadPalette palette, IColorDistance<RgbVector> colorDistance)
        {
            BeadPattern convertedPattern = new BeadPattern(Width, Height);
            for (int i = 0; i < Width; ++i)
            {
                for (int j = 0; j < Height; ++j)
                {
                    Color color = beads[i, j].Color;
                    RgbVector rgb = new RgbVector(color);
                    Bead bestFit = FindBestFittingBead(rgb, palette, colorDistance);
                    convertedPattern.beads[i, j] = bestFit;
                }
            }
            return convertedPattern;
        }

        private static Bead FindBestFittingBead(RgbVector rgb, BeadPalette palette, IColorDistance<RgbVector> colorDistance)
        {
            double minimumDistance = double.MaxValue;
            Bead bestFit = null;

            foreach (var bead in palette.Beads)
            {
                var v = new RgbVector(bead.Color);
                double d = colorDistance.Between(rgb, v);
                if (d < minimumDistance)
                {
                    minimumDistance = d;
                    bestFit = bead;
                }
            }

            return bestFit;
        }
    }
}