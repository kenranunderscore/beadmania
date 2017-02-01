using beadmania.Logic.Converters;
using beadmania.Logic.ColorVectors;
using NUnit.Framework;

namespace beadmania.Logic.UnitTests.Converters
{
    [TestFixture]
    public class XyzToLabConverterTest
    {
        [Test]
        public void Zero_vector_is_converted_to_zero()
        {
            XyzVector xyz = new XyzVector(0d, 0d, 0d);
            XyzToLabConverter converter = new XyzToLabConverter();
            LabVector zero = new LabVector(0d, 0d, 0d);
            Assert.That(converter.Convert(xyz), Is.EqualTo(zero));
        }

        [Test]
        public void Arbitrary_XYZ_vector_is_converted_correctly_to_Lab()
        {
            XyzVector xyz = new XyzVector(38.06d, 63.24d, 12.68d);
            XyzToLabConverter converter = new XyzToLabConverter();
            LabVector lab = converter.Convert(xyz);

            Assert.That(lab.X, Is.EqualTo(83.57d).Within(0.01d));
            Assert.That(lab.Y, Is.EqualTo(-60.64d).Within(0.01d));
            Assert.That(lab.Z, Is.EqualTo(74d).Within(0.01d));
        }
    }
}