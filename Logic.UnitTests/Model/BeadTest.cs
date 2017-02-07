namespace beadmania.Logic.UnitTests.Model
{
    using System.Drawing;
    using beadmania.Logic.Model;
    using NUnit.Framework;

    [TestFixture]
    public class BeadTest
    {
        [Test]
        public void Description_is_initially_null()
        {
            Bead bead = new Bead();
            Assert.That(bead.Description, Is.Null);
        }

        [Test]
        public void Can_set_description()
        {
            Bead bead = new Bead();
            bead.Description = "foo";
            Assert.That(bead.Description, Is.EqualTo("foo"));
        }

        [Test]
        public void Color_is_initially_empty()
        {
            Bead bead = new Bead();
            Assert.That(bead.Color, Is.EqualTo(Color.Empty));
        }

        [Test]
        public void Can_set_color()
        {
            Bead bead = new Bead();
            bead.Color = Color.HotPink;
            Assert.That(bead.Color, Is.EqualTo(Color.HotPink));
        }

        [Test]
        public void Beads_are_equal_if_color_and_description_coincide()
        {
            Bead bead1 = new Bead { Description = "Abc", Color = Color.FromArgb(55, 10, 20, 30) };
            Bead bead2 = new Bead { Description = "Abc", Color = Color.FromArgb(55, 10, 20, 30) };
            Assert.That(bead1, Is.EqualTo(bead2));
        }

        [Test]
        public void Two_beads_are_object_equal_when_they_are_equal_via_IEquatable()
        {
            Bead bead1 = new Bead();
            Bead bead2 = new Bead();
            Assert.That(bead1.Equals((object)bead2));
        }

        [Test]
        public void Two_beads_with_different_colors_are_not_equal()
        {
            Bead bead1 = new Bead { Color = Color.Black };
            Bead bead2 = new Bead { Color = Color.Beige };
            Assert.That(bead1, Is.Not.EqualTo(bead2));
        }

        [Test]
        public void Null_is_not_equal_to_a_bead_instance()
        {
            Bead bead = new Bead();
            Assert.That(bead, Is.Not.EqualTo(null));
        }

        [Test]
        public void Null_is_not_equal_to_a_vector_instance_when_comparing_via_IEquatable()
        {
            Bead bead = new Bead();
            Assert.That(bead.Equals(null), Is.False);
        }

        [Test]
        public void Two_distinct_references_of_equal_vectors_have_identical_hash_codes()
        {
            Bead bead1 = new Bead { Description = "A", Color = Color.Aqua };
            Bead bead2 = new Bead { Description = "A", Color = Color.Aqua };
            Assert.That(bead1.GetHashCode(), Is.EqualTo(bead2.GetHashCode()));
        }

        [Test]
        public void A_bead_is_not_equal_to_instances_of_another_class()
        {
            Bead bead = new Bead();
            Assert.That(bead, Is.Not.EqualTo("abc"));
        }

        [Test]
        public void A_cloned_bead_is_a_new_reference()
        {
            Bead bead = new Bead();
            Bead clone = bead.Clone();
            Assert.That(clone, Is.Not.SameAs(bead));
        }

        [Test]
        public void A_cloned_bead_retains_its_color()
        {
            Bead bead = new Bead { Color = Color.Aquamarine };
            Bead clone = bead.Clone();
            Assert.That(clone.Color, Is.EqualTo(bead.Color));
        }

        [Test]
        public void A_cloned_bead_retains_its_description()
        {
            Bead bead = new Bead { Description = "FooBead" };
            Bead clone = bead.Clone();
            Assert.That(clone.Description, Is.EqualTo(bead.Description));
        }
    }
}