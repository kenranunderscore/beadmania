using beadmania.Logic.Converters;
using beadmania.Logic.ColorVectors;
using NUnit.Framework;
using System;
using System.Drawing;

namespace beadmania.Logic.UnitTests.ColorVectors
{
    [TestFixture]
    public class RgbVectorTest
    {
        [Test]
        public void Negative_R_value_throws()
        {
            Assert.That(() => new RgbVector(-1, 25, 0), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Negative_G_value_throws()
        {
            Assert.That(() => new RgbVector(31, -5, 0), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Negative_B_value_throws()
        {
            Assert.That(() => new RgbVector(31, 5, -200), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void R_value_greater_than_255_throws()
        {
            Assert.That(() => new RgbVector(256, 5, 3), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void G_value_greater_than_255_throws()
        {
            Assert.That(() => new RgbVector(6, 256, 3), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void B_value_greater_than_255_throws()
        {
            Assert.That(() => new RgbVector(0, 0, 2574), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Constructing_from_color_keeps_R_value()
        {
            RgbVector rgb = new RgbVector(Color.FromArgb(0, 3, 5));
            Assert.That(rgb.X, Is.EqualTo(0));
        }

        [Test]
        public void Constructing_from_color_keeps_G_value()
        {
            RgbVector rgb = new RgbVector(Color.FromArgb(0, 3, 5));
            Assert.That(rgb.Y, Is.EqualTo(3));
        }

        [Test]
        public void Constructing_from_color_keeps_B_value()
        {
            RgbVector rgb = new RgbVector(Color.FromArgb(0, 3, 5));
            Assert.That(rgb.Z, Is.EqualTo(5));
        }

        [Test]
        public void Conversion_to_XYZ_is_identical_to_converter_result()
        {
            RgbVector rgb = new RgbVector(255, 0, 13);
            RgbToXyzConverter converter = new RgbToXyzConverter();
            XyzVector xyz = converter.Convert(rgb);
            Assert.That(rgb.ToXyz(), Is.EqualTo(xyz));
        }
    }
}