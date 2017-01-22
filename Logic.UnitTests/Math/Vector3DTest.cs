using beadmania.Logic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace beadmania.Logic.UnitTests.Math
{
    [TestClass]
    public class Vector3DTest
    {
        private Vector3D v1 = new Vector3D(1d, 2d, 3.5d);

        [TestMethod]
        public void X_returns_first_coordinate()
        {
            Assert.AreEqual(1d, v1.X);
        }

        [TestMethod]
        public void Y_returns_second_coordinate()
        {
            Assert.AreEqual(2d, v1.Y);
        }

        [TestMethod]
        public void Z_returns_third_coordinate()
        {
            Assert.AreEqual(3.5d, v1.Z);
        }

        [TestMethod]
        public void ToString_returns_human_readable_representation()
        {
            Assert.AreEqual("x=1;y=2;z=3.5", v1.ToString());
        }
    }
}