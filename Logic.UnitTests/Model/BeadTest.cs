using beadmania.Logic.Model;
using NUnit.Framework;
using System.Drawing;

namespace beadmania.Logic.UnitTests.Model
{
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
    }
}