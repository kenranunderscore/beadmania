﻿namespace beadmania.Logic.UnitTests.Model
{
    using System.Drawing;
    using System.Linq;
    using System.Xml.Linq;
    using beadmania.Logic.Model;
    using NUnit.Framework;

    [TestFixture]
    public class BeadPaletteTest
    {
        [Test]
        public void New_palette_is_initially_empty()
        {
            BeadPalette palette = new BeadPalette("Foo");
            Assert.That(palette.Beads, Is.Empty);
        }

        [Test]
        public void Added_beads_are_contained_in_the_palette()
        {
            BeadPalette palette = new BeadPalette("Foo");
            Bead bead = new Bead();
            palette.Add(bead);
            Assert.That(palette.Beads, Contains.Item(bead));
        }

        [Test]
        public void Doubly_added_beads_are_only_contained_once()
        {
            BeadPalette palette = new BeadPalette("Foo");
            palette.Add(new Bead());
            palette.Add(new Bead());
            Assert.That(palette.Beads, Contains.Item(new Bead()).With.Count.EqualTo(1));
        }

        [Test]
        public void Empty_palette_produces_XML_with_only_a_root_node()
        {
            BeadPalette palette = new BeadPalette("Foo");
            Assert.That(palette.ToXml().ToString(), Is.EqualTo("<BeadPalette Name=\"Foo\" />"));
        }

        [Test]
        public void Correct_number_of_beads_is_saved()
        {
            BeadPalette palette = new BeadPalette("Foo");
            palette.Add(new Bead { Identifier = "Check", Color = Color.Firebrick });
            palette.Add(new Bead { Identifier = "Mate", Color = Color.DeepSkyBlue });
            var numberOfBeads = palette.ToXml()
                .Descendants(nameof(Bead))
                .Count();
            Assert.That(numberOfBeads, Is.EqualTo(2));
        }

        [Test]
        public void Palette_loaded_from_XML_has_correct_name()
        {
            const string xml = "<BeadPalette Name=\"Jelly\" />";
            var palette = BeadPalette.FromXml(XDocument.Parse(xml));
            Assert.That(palette.Name, Is.EqualTo("Jelly"));
        }

        [Test]
        public void Cannot_load_from_XML_that_has_not_specified_palette_name()
        {
            const string xml = "<BeadPalette />";
            Assert.That(() => BeadPalette.FromXml(XDocument.Parse(xml)), Throws.Exception);
        }

        [Test]
        public void Beads_in_XML_are_contained_in_loaded_palette()
        {
            const string xml =
                @"<BeadPalette Name=""Abc"">
                    <Bead>
                        <Identifier>MrBead</Identifier>
                        <Color>#ff00ff</Color>
                    </Bead>
                </BeadPalette>";
            var palette = BeadPalette.FromXml(XDocument.Parse(xml));
            Bead expectedBead = new Bead { Identifier = "MrBead", Color = Color.FromArgb(255, 0, 255) };
            Assert.That(palette.Beads, Contains.Item(expectedBead));
        }

        [Test]
        public void A_cloned_palette_is_a_new_reference()
        {
            BeadPalette palette = new BeadPalette("Foo");
            var clone = palette.Clone();
            Assert.That(clone, Is.Not.SameAs(palette));
        }

        [Test]
        public void A_cloned_palette_retains_its_name()
        {
            BeadPalette palette = new BeadPalette("FooPalette");
            var clone = palette.Clone();
            Assert.That(clone.Name, Is.EqualTo(palette.Name));
        }

        [Test]
        public void A_cloned_palette_retains_its_bead_count()
        {
            BeadPalette palette = new BeadPalette("FooPalette");
            palette.Add(new Bead { Identifier = "A", Color = Color.DarkOrchid });
            palette.Add(new Bead { Identifier = "B", Color = Color.GreenYellow });
            var clone = palette.Clone();
            Assert.That(clone.Beads.Count(), Is.EqualTo(palette.Beads.Count()));
        }

        [Test]
        public void A_cloned_palette_produces_XML_identical_to_the_original()
        {
            BeadPalette palette = new BeadPalette("FooPalette");
            palette.Add(new Bead { Identifier = "A", Color = Color.DarkOrchid });
            palette.Add(new Bead { Identifier = "B", Color = Color.GreenYellow });
            var clone = palette.Clone();
            Assert.That(clone.ToXml().ToString(), Is.EqualTo(palette.ToXml().ToString()));
        }
    }
}