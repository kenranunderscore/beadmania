using beadmania.Logic.Converters;
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

        [Test]
        public void Conversion_to_Lab_is_identical_to_converter_result()
        {
            XyzVector xyz = new XyzVector(3.5d, 3.14159d, 99.9d);
            XyzToLabConverter converter = new XyzToLabConverter();
            LabVector lab = converter.Convert(xyz);
            Assert.That(xyz.ToLab(), Is.EqualTo(lab));
        }
    }
}