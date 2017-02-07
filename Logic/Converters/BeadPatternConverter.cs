namespace beadmania.Logic.Converters
{
    using System.Drawing;
    using beadmania.Logic.ColorVectors;
    using beadmania.Logic.Delta;
    using beadmania.Logic.Model;

    public class BeadPatternConverter
    {
        private readonly IColorDistance<RgbVector> colorDistance;
        private readonly BeadPalette palette;

        public BeadPatternConverter(BeadPalette palette, IColorDistance<RgbVector> colorDistance)
        {
            this.colorDistance = colorDistance;
            this.palette = palette;
        }

        public BeadPattern Convert(BeadPattern source)
        {
            BeadPattern convertedPattern = new BeadPattern(source.Width, source.Height);
            for (int i = 0; i < source.Width; ++i)
            {
                for (int j = 0; j < source.Height; ++j)
                {
                    Color color = source[i, j].Color;
                    RgbVector rgb = new RgbVector(color);
                    Bead bestFit = FindBestFittingBead(rgb);
                    convertedPattern[i, j] = bestFit;
                }
            }
            return convertedPattern;
        }

        private Bead FindBestFittingBead(RgbVector rgb)
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