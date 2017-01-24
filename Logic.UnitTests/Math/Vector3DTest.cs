using beadmania.Logic.Math;
using NUnit.Framework;
using System;

namespace beadmania.Logic.UnitTests.Math
{
    [TestFixture]
    public class Vector3DTest
    {
        private Vector3D v = new Vector3D(1d, 2d, 3.5d);

        [Test]
        public void X_returns_first_coordinate()
        {
            Assert.AreEqual(1d, v.X);
        }

        [Test]
        public void Y_returns_second_coordinate()
        {
            Assert.AreEqual(2d, v.Y);
        }

        [Test]
        public void Z_returns_third_coordinate()
        {
            Assert.AreEqual(3.5d, v.Z);
        }

        [Test]
        public void ToString_returns_human_readable_representation()
        {
            Assert.AreEqual("x=1;y=2;z=3.5", v.ToString());
        }

        [Test]
        public void Two_vectors_are_equal_if_their_coordinates_coincide()
        {
            Vector3D w = new Vector3D(1d, 2d, 3.5d);
            Assert.IsTrue(v.Equals(w));
        }

        [Test]
        public void Two_vectors_are_object_equal_if_their_coordinates_coincide()
        {
            Vector3D w = new Vector3D(1d, 2d, 3.5d);
            Assert.IsTrue(Equals(v, w));
        }

        [Test]
        public void Two_vectors_with_different_coordinates_are_not_equal()
        {
            Vector3D w = new Vector3D(-1d, -1d, -1d);
            Assert.IsFalse(v.Equals(w));
        }

        [Test]
        public void Null_is_not_equal_to_a_vector_instance()
        {
            Assert.IsFalse(v.Equals(null));
        }

        [Test]
        public void Two_distinct_references_of_equal_vectors_have_identical_hash_codes()
        {
            Vector3D w = new Vector3D(1d, 2d, 3.5d);
            Assert.IsTrue(v.GetHashCode() == w.GetHashCode());
        }

        [Test]
        public void A_vector_is_not_equal_to_instances_of_another_class()
        {
            Assert.IsFalse(v.Equals("abc"));
        }

        [Test]
        public void Addition_of_two_vectors_adds_coordinates()
        {
            Vector3D w = new Vector3D(-0.5d, 1d, -4d);
            Assert.AreEqual(new Vector3D(0.5d, 3d, -0.5d), v + w);
        }

        [Test]
        public void Subtraction_of_two_vectors_subtracts_coordinates()
        {
            Vector3D w = new Vector3D(-0.5d, 1d, -4d);
            Assert.AreEqual(new Vector3D(1.5d, 1d, 7.5d), v - w);
        }

        [Test]
        public void Scalar_multiplication_works_coordinate_wise()
        {
            Assert.AreEqual(new Vector3D(-2d, -4d, -7d), (-2d) * v);
        }

        [Test]
        public void Array_constructor_throws_when_dimension_is_wrong()
        {
            Action create = () => new Vector3D(new double[] { 1d, 2d, 3d, 4d });
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(create));
        }

        [Test]
        public void Array_and_normal_constructors_have_same_result()
        {
            Vector3D w = new Vector3D(new double[] { 1d, 2d, 3.5d });
        }
    }
}