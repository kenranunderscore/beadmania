using beadmania.Logic.ColorVectors;
using NUnit.Framework;

namespace beadmania.Logic.UnitTests.ColorVectors
{
    [TestFixture]
    public class XyzVectorTest
    {
        [Test]
        public void First_coordinate_carries_over_when_constructing()
        {
            XyzVector v = new XyzVector(3d, 4d, 5d);
            Assert.That(v.X, Is.EqualTo(3d));
        }

        [Test]
        public void Second_coordinate_carries_over_when_constructing()
        {
            XyzVector v = new XyzVector(3d, 4d, 5d);
            Assert.That(v.Y, Is.EqualTo(4d));
        }

        [Test]
        public void Third_coordinate_carries_over_when_constructing()
        {
            XyzVector v = new XyzVector(3d, 4d, 5d);
            Assert.That(v.Z, Is.EqualTo(5d));
        }
    }
}