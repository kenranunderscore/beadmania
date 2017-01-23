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
    }
}