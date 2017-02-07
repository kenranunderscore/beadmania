namespace beadmania.Logic.UnitTests.Converters
{
    using System.Drawing;
    using beadmania.Logic.ColorVectors;
    using beadmania.Logic.Converters;
    using beadmania.Logic.Delta;
    using beadmania.Logic.Model;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BeadPatternConverterTest
    {
        [Test]
        public void Converted_pattern_keeps_width()
        {
            Bitmap bmp = new Bitmap(2, 2);
            BeadPattern pattern = BeadPattern.FromBitmap(bmp);
            BeadPalette palette = new BeadPalette("foo");
            var convertedPattern = new BeadPatternConverter(palette, new EuclideanDistance()).Convert(pattern);
            Assert.That(convertedPattern.Width, Is.EqualTo(pattern.Width));
        }

        [Test]
        public void Converted_pattern_keeps_height()
        {
            Bitmap bmp = new Bitmap(2, 2);
            BeadPattern pattern = BeadPattern.FromBitmap(bmp);
            BeadPalette palette = new BeadPalette("foo");
            var convertedPattern = new BeadPatternConverter(palette, new EuclideanDistance()).Convert(pattern);
            Assert.That(convertedPattern.Height, Is.EqualTo(pattern.Height));
        }

        [Test]
        public void Conversion_algorithm_calculates_color_distance_once_per_point_and_palette_bead()
        {
            Bitmap bmp = new Bitmap(2, 2);
            BeadPattern pattern = BeadPattern.FromBitmap(bmp);
            BeadPalette palette = new BeadPalette("foo");
            palette.Add(new Bead { Description = "Black", Color = Color.Black });
            palette.Add(new Bead { Description = "Gray", Color = Color.Gray });
            var colorDistanceMock = new Mock<IColorDistance<RgbVector>>();
            BeadPatternConverter converter = new BeadPatternConverter(palette, colorDistanceMock.Object);
            var convertedPattern = converter.Convert(pattern);
            colorDistanceMock.Verify(_ => _.Between(It.IsAny<RgbVector>(), It.IsAny<RgbVector>()), Times.Exactly(8));
        }
    }
}