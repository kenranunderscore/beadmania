﻿using beadmania.Logic.ColorSpaceConverters;
using beadmania.Logic.ColorVectors;
using NUnit.Framework;
using System.Drawing;

namespace beadmania.Logic.UnitTests.ColorSpaceConverters
{
    [TestFixture]
    public class RgbToXyzConverterTest
    {
        [Test]
        public void Black_is_converted_to_xyz_correctly()
        {
            RgbVector black = new RgbVector(Color.Black);
            RgbToXyzConverter converter = new RgbToXyzConverter();
            XyzVector zero = new XyzVector(0d, 0d, 0d);
            Assert.That(converter.Convert(black), Is.EqualTo(zero));
        }

        [Test]
        public void White_is_converted_to_xyz_correctly()
        {
            RgbVector white = new RgbVector(Color.White);
            RgbToXyzConverter converter = new RgbToXyzConverter();
            XyzVector v = converter.Convert(white);

            Assert.That(v.X, Is.EqualTo(95.05d).Within(0.01));
            Assert.That(v.Y, Is.EqualTo(100d).Within(0.01));
            Assert.That(v.Z, Is.EqualTo(108.9d).Within(0.01));
        }

        [Test]
        public void Arbitrary_color_is_converted_correctly()
        {
            RgbVector white = new RgbVector(154, 3, 240);
            RgbToXyzConverter converter = new RgbToXyzConverter();
            XyzVector v = converter.Convert(white);

            Assert.That(v.X, Is.EqualTo(29.08d).Within(0.01));
            Assert.That(v.Y, Is.EqualTo(13.23d).Within(0.01));
            Assert.That(v.Z, Is.EqualTo(83.46d).Within(0.01));
        }
    }
}