using beadmania.Logic.ColorVectors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadmania.Logic.UnitTests.ColorVectors
{
    [TestClass]
    public class RgbVectorTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Negative_R_value_throws()
        {
            RgbVector rgb = new RgbVector(-1d, 25d, 0d);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Negative_G_value_throws()
        {
            RgbVector rgb = new RgbVector(31d, -5d, 0d);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Negative_B_value_throws()
        {
            RgbVector rgb = new RgbVector(31d, 5d, -200d);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void R_value_greater_than_255_throws()
        {
            RgbVector rgb = new RgbVector(256d, 5d, 3d);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void G_value_greater_than_255_throws()
        {
            RgbVector rgb = new RgbVector(6d, 256d, 3d);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void B_value_greater_than_255_throws()
        {
            RgbVector rgb = new RgbVector(0d, 0d, 2574d);
        }

        [TestMethod]
        public void Constructing_from_color_keeps_R_value()
        {
            RgbVector rgb = new RgbVector(Color.FromArgb(0, 3, 5));
            Assert.AreEqual(0, rgb.X);
        }

        [TestMethod]
        public void Constructing_from_color_keeps_G_value()
        {
            RgbVector rgb = new RgbVector(Color.FromArgb(0, 3, 5));
            Assert.AreEqual(3, rgb.Y);
        }

        [TestMethod]
        public void Constructing_from_color_keeps_B_value()
        {
            RgbVector rgb = new RgbVector(Color.FromArgb(0, 3, 5));
            Assert.AreEqual(5, rgb.Z);
        }
    }
}