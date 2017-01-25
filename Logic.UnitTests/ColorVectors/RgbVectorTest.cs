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
            Action construct = () => new RgbVector(-1, 25, 0);
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(construct));
        }

        [Test]
        public void Negative_G_value_throws()
        {
            Action construct = () => new RgbVector(31, -5, 0);
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(construct));
        }

        [Test]
        public void Negative_B_value_throws()
        {
            Action construct = () => new RgbVector(31, 5, -200);
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(construct));
        }

        [Test]
        public void R_value_greater_than_255_throws()
        {
            Action create = () => new RgbVector(256, 5, 3);
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(create));
        }

        [Test]
        public void G_value_greater_than_255_throws()
        {
            Action create = () => new RgbVector(6, 256, 3);
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(create));
        }

        [Test]
        public void B_value_greater_than_255_throws()
        {
            Action create = () => new RgbVector(0, 0, 2574);
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(create));
        }

        [Test]
        public void Constructing_from_color_keeps_R_value()
        {
            RgbVector rgb = new RgbVector(Color.FromArgb(0, 3, 5));
            Assert.AreEqual(0, rgb.X);
        }

        [Test]
        public void Constructing_from_color_keeps_G_value()
        {
            RgbVector rgb = new RgbVector(Color.FromArgb(0, 3, 5));
            Assert.AreEqual(3, rgb.Y);
        }

        [Test]
        public void Constructing_from_color_keeps_B_value()
        {
            RgbVector rgb = new RgbVector(Color.FromArgb(0, 3, 5));
            Assert.AreEqual(5, rgb.Z);
        }
    }
}