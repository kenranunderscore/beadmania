namespace beadmania.Logic.UnitTests.Math
{
    using System;
    using beadmania.Logic.Math;
    using NUnit.Framework;

    [TestFixture]
    public class Vector3DTest
    {
        private Vector3D v = new Vector3D(1d, 2d, 3.5d);

        [Test]
        public void X_returns_first_coordinate()
        {
            Assert.That(v.X, Is.EqualTo(1d));
        }

        [Test]
        public void Y_returns_second_coordinate()
        {
            Assert.That(v.Y, Is.EqualTo(2d));
        }

        [Test]
        public void Z_returns_third_coordinate()
        {
            Assert.That(v.Z, Is.EqualTo(3.5d));
        }

        [Test]
        public void ToString_returns_human_readable_representation()
        {
            Assert.That(v.ToString(), Is.EqualTo("x=1;y=2;z=3.5"));
        }

        [Test]
        public void Two_vectors_are_equal_if_their_coordinates_coincide()
        {
            Vector3D w = new Vector3D(1d, 2d, 3.5d);
            Assert.That(v.Equals(w));
        }

        [Test]
        public void Two_vectors_are_object_equal_if_their_coordinates_coincide()
        {
            Vector3D w = new Vector3D(1d, 2d, 3.5d);
            Assert.That(v.Equals((object)w));
        }

        [Test]
        public void Two_vectors_with_different_coordinates_are_not_equal()
        {
            Vector3D w = new Vector3D(-1d, -1d, -1d);
            Assert.That(v, Is.Not.EqualTo(w));
        }

        [Test]
        public void Null_is_not_equal_to_a_vector_instance()
        {
            Assert.That(v, Is.Not.EqualTo(null));
        }

        [Test]
        public void Null_is_not_equal_to_a_vector_instance_when_comparing_via_IEquatable()
        {
            Assert.That(v.Equals(null), Is.False);
        }

        [Test]
        public void Two_distinct_references_of_equal_vectors_have_identical_hash_codes()
        {
            Vector3D w = new Vector3D(1d, 2d, 3.5d);
            Assert.That(w.GetHashCode(), Is.EqualTo(v.GetHashCode()));
        }

        [Test]
        public void A_vector_is_not_equal_to_instances_of_another_class()
        {
            Assert.That(v, Is.Not.EqualTo("abc"));
        }

        [Test]
        public void Addition_of_two_vectors_adds_coordinates()
        {
            Vector3D w = new Vector3D(-0.5d, 1d, -4d);
            Assert.That(v + w, Is.EqualTo(new Vector3D(0.5d, 3d, -0.5d)));
        }

        [Test]
        public void Subtraction_of_two_vectors_subtracts_coordinates()
        {
            Vector3D w = new Vector3D(-0.5d, 1d, -4d);
            Assert.That(v - w, Is.EqualTo(new Vector3D(1.5d, 1d, 7.5d)));
        }

        [Test]
        public void Scalar_multiplication_works_coordinate_wise()
        {
            Assert.That((-2d) * v, Is.EqualTo(new Vector3D(-2d, -4d, -7d)));
        }

        [Test]
        public void Array_constructor_throws_when_dimension_is_wrong()
        {
            Assert.That(() => new Vector3D(new double[] { 1d, 2d, 3d, 4d }), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Array_and_normal_constructors_have_same_result()
        {
            Vector3D w = new Vector3D(new double[] { 1d, 2d, 3.5d });
            Assert.That(w, Is.EqualTo(v));
        }
    }
}