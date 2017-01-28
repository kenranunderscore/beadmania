using beadmania.Logic.Model;
using NUnit.Framework;

namespace beadmania.Logic.UnitTests.Model
{
    [TestFixture]
    public class BeadPaletteTest
    {
        [Test]
        public void New_palette_is_initially_empty()
        {
            BeadPalette palette = new BeadPalette();
            Assert.That(palette.Beads, Is.Empty);
        }

        [Test]
        public void Added_beads_are_contained_in_the_palette()
        {
            BeadPalette palette = new BeadPalette();
            Bead bead = new Bead();
            palette.Add(bead);
            Assert.That(palette.Beads, Contains.Item(bead));
        }
    }
}