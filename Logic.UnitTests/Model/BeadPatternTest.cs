using beadmania.Logic.Model;
using NUnit.Framework;
using System.Drawing;

namespace beadmania.Logic.UnitTests.Model
{
    [TestFixture]
    public class BeadPatternTest
    {
        [Test]
        public void Creating_from_bitmap_sets_width_to_bitmap_width()
        {
            Bitmap bmp = new Bitmap(1, 3);
            BeadPattern pattern = BeadPattern.FromBitmap(bmp);
            Assert.That(pattern.Width, Is.EqualTo(1));
        }

        [Test]
        public void Creating_from_bitmap_sets_height_to_bitmap_height()
        {
            Bitmap bmp = new Bitmap(1, 3);
            BeadPattern pattern = BeadPattern.FromBitmap(bmp);
            Assert.That(pattern.Height, Is.EqualTo(3));
        }

        [Test]
        public void Indexer_returns_bead_with_correct_color()
        {
            Bitmap bmp = new Bitmap(2, 2);
            bmp.SetPixel(1, 1, Color.ForestGreen);
            BeadPattern pattern = BeadPattern.FromBitmap(bmp);
            Assert.That(pattern[1, 1].Color.ToArgb(), Is.EqualTo(Color.ForestGreen.ToArgb()));
        }
    }
}