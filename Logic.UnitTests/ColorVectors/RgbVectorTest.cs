using beadmania.Logic.ColorVectors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    }
}