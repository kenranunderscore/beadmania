using beadmania.Logic.Model;
using NUnit.Framework;
using System.Drawing;
using System.Linq;

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

        [Test]
        public void Empty_palette_produces_XML_with_only_a_root_node()
        {
            BeadPalette palette = new BeadPalette();
            Assert.That(palette.ToXml().ToString(), Is.EqualTo("<BeadPalette />"));
        }

        [Test]
        public void Correct_number_of_beads_is_saved()
        {
            BeadPalette palette = new BeadPalette();
            palette.Add(new Bead { Description = "Check", Color = Color.Firebrick });
            palette.Add(new Bead { Description = "Mate", Color = Color.DeepSkyBlue });
            var numberOfBeads = palette.ToXml()
                .Descendants(nameof(Bead))
                .Count();
            Assert.That(numberOfBeads, Is.EqualTo(2));
        }
    }
}