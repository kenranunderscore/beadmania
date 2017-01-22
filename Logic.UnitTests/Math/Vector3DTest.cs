using beadmania.Logic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace beadmania.Logic.UnitTests.Math
{
    [TestClass]
    public class Vector3DTest
    {
        private Vector3D v = new Vector3D(1d, 2d, 3.5d);

        [TestMethod]
        public void XTest()
        {
            Assert.AreEqual(1d, v.X);
        }

        [TestMethod]
        public void YTest()
        {
            Assert.AreEqual(2d, v.Y);
        }

        [TestMethod]
        public void ZTest()
        {
            Assert.AreEqual(3.5d, v.Z);
        }
    }
}