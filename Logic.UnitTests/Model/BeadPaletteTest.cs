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

        [Test]
        public void Doubly_added_beads_are_only_contained_once()
        {
            BeadPalette palette = new BeadPalette();
            palette.Add(new Bead());
            palette.Add(new Bead());
            Assert.That(palette.Beads, Contains.Item(new Bead()).With.Count.EqualTo(1));
        }
    }
}