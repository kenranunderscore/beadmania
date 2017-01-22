using beadmania.Logic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace beadmania.Logic.UnitTests.Math
{
    [TestClass]
    public class Vector3DTest
    {
        private Vector3D v = new Vector3D(1d, 2d, 3.5d);

        [TestMethod]
        public void X_returns_first_coordinate()
        {
            Assert.AreEqual(1d, v.X);
        }

        [TestMethod]
        public void Y_returns_second_coordinate()
        {
            Assert.AreEqual(2d, v.Y);
        }

        [TestMethod]
        public void Z_returns_third_coordinate()
        {
            Assert.AreEqual(3.5d, v.Z);
        }

        [TestMethod]
        public void ToString_returns_human_readable_representation()
        {
            Assert.AreEqual("x=1;y=2;z=3.5", v.ToString());
        }

        [TestMethod]
        public void Two_vectors_are_equal_if_their_coordinates_coincide()
        {
            Vector3D w = new Vector3D(1d, 2d, 3.5d);
            Assert.IsTrue(v.Equals(w));
        }

        [TestMethod]
        public void Two_vectors_are_object_equal_if_their_coordinates_coincide()
        {
            Vector3D w = new Vector3D(1d, 2d, 3.5d);
            Assert.IsTrue(Equals(v, w));
        }

        [TestMethod]
        public void Null_is_not_equal_to_a_vector_instance()
        {
            Assert.IsFalse(v.Equals(null));
        }

        [TestMethod]
        public void Two_distinct_references_of_equal_vectors_have_identical_hash_codes()
        {
            Vector3D w = new Vector3D(1d, 2d, 3.5d);
            Assert.IsTrue(v.GetHashCode() == w.GetHashCode());
        }

        [TestMethod]
        public void A_vector_is_not_equal_to_instances_of_another_class()
        {
            Assert.IsFalse(v.Equals("abc"));
        }

        [TestMethod]
        public void Addition_of_two_vectors_adds_coordinates()
        {
            Vector3D w = new Vector3D(-0.5d, 1d, -4d);
            Assert.AreEqual(new Vector3D(0.5d, 3d, -0.5d), v + w);
        }

        [TestMethod]
        public void Subtraction_of_two_vectors_subtracts_coordinates()
        {
            Vector3D w = new Vector3D(-0.5d, 1d, -4d);
            Assert.AreEqual(new Vector3D(1.5d, 1d, 7.5d), v - w);
        }

        [TestMethod]
        public void Scalar_multiplication_works_coordinate_wise()
        {
            Assert.AreEqual(new Vector3D(-2d, -4d, -7d), (-2d) * v);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Array_constructor_throws_when_dimension_is_wrong()
        {
            Vector3D w = new Vector3D(new double[] { 1d, 2d, 3d, 4d });
        }
    }
}