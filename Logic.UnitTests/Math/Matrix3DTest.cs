using beadmania.Logic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace beadmania.Logic.UnitTests.Math
{
    [TestClass]
    public class Matrix3DTest
    {
        private Matrix3D m = new Matrix3D(new double[,]
        {
            { -1d, 0d, 0.5d },
            { 0d, 2d, 0d },
            { 1d, -1d, 0d },
        });

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_throws_if_first_dimension_is_not_3()
        {
            var array = new double[2, 3];
            Matrix3D matrix = new Matrix3D(array);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_throws_if_second_dimension_is_not_3()
        {
            var array = new double[3, 7];
            Matrix3D matrix = new Matrix3D(array);
        }

        [TestMethod]
        public void Two_matrices_are_equal_if_all_their_entries_coincide()
        {
            Matrix3D n = new Matrix3D(new double[,]
            {
                { -1d, 0d, 0.5d },
                { 0d, 2d, 0d },
                { 1d, -1d, 0d },
            });
            Assert.IsTrue(m.Equals(n));
        }

        [TestMethod]
        public void Two_matrices_are_object_equal_if_all_their_entries_coincide()
        {
            Matrix3D n = new Matrix3D(new double[,]
            {
                { -1d, 0d, 0.5d },
                { 0d, 2d, 0d },
                { 1d, -1d, 0d },
            });
            Assert.IsTrue(Equals(m, n));
        }

        [TestMethod]
        public void Null_is_not_equal_to_a_matrix_instance()
        {
            Assert.IsFalse(m.Equals(null));
        }
    }
}