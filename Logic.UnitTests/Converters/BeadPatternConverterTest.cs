using beadmania.Logic.Converters;
using beadmania.Logic.Delta;
using beadmania.Logic.Model;
using NUnit.Framework;
using System.Drawing;

namespace beadmania.Logic.UnitTests.Converters
{
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
    }
}