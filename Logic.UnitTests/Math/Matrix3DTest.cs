namespace beadmania.Logic.UnitTests.Math
{
    using System;
    using beadmania.Logic.Math;
    using NUnit.Framework;

    [TestFixture]
    public class Matrix3DTest
    {
        private Matrix3D m = new Matrix3D(new double[,]
        {
            { -1d, 0d, 0.5d },
            { 0d, 2d, 0d },
            { 1d, -1d, 0d }
        });

        [Test]
        public void Constructor_throws_if_first_dimension_is_not_3()
        {
            var array = new double[2, 3];
            Assert.That(() => new Matrix3D(array), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Constructor_throws_if_second_dimension_is_not_3()
        {
            var array = new double[3, 7];
            Assert.That(() => new Matrix3D(array), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Two_matrices_are_equal_if_all_their_entries_coincide()
        {
            Matrix3D n = new Matrix3D(new double[,]
            {
                { -1d, 0d, 0.5d },
                { 0d, 2d, 0d },
                { 1d, -1d, 0d }
            });
            Assert.That(m.Equals(n));
        }

        [Test]
        public void Two_matrices_are_object_equal_if_all_their_entries_coincide()
        {
            Matrix3D n = new Matrix3D(new double[,]
            {
                { -1d, 0d, 0.5d },
                { 0d, 2d, 0d },
                { 1d, -1d, 0d }
            });
            Assert.That(m.Equals((object)n));
        }

        [Test]
        public void Two_matrices_with_different_entries_are_not_equal()
        {
            Matrix3D n = new Matrix3D(new double[,]
            {
                { -1d, 0d, 0.5d },
                { 0d, 2d, 0d },
                { 1d, 1d, 0d }
            });
            Assert.That(m, Is.Not.EqualTo(n));
        }

        [Test]
        public void Null_is_not_equal_to_a_matrix_instance()
        {
            Assert.That(m, Is.Not.EqualTo(null));
        }

        [Test]
        public void A_matrix_is_not_equal_to_instances_of_another_class()
        {
            Assert.That(m, Is.Not.EqualTo("abc"));
        }

        [Test]
        public void Two_distinct_references_of_equal_matrices_have_identical_hash_codes()
        {
            Matrix3D n = new Matrix3D(new double[,]
            {
                { -1d, 0d, 0.5d },
                { 0d, 2d, 0d },
                { 1d, -1d, 0d }
            });
            Assert.That(n.GetHashCode(), Is.EqualTo(n.GetHashCode()));
        }

        [Test]
        public void Addition_of_matrices_adds_entries()
        {
            Matrix3D n = new Matrix3D(new double[,]
            {
                { 1d, 1d, 1d },
                { 1d, 1d, 1d },
                { 1d, 1d, 1d }
            });
            Matrix3D expected = new Matrix3D(new double[,]
            {
                { 0d, 1d, 1.5d },
                { 1d, 3d, 1d },
                { 2d, 0d, 1d }
            });
            Assert.That(m + n, Is.EqualTo(expected));
        }

        [Test]
        public void Subtraction_of_matrices_subtracts_entries()
        {
            Matrix3D n = new Matrix3D(new double[,]
            {
                { 1d, 1d, 1d },
                { 1d, 1d, 1d },
                { 1d, 1d, 1d }
            });
            Matrix3D expected = new Matrix3D(new double[,]
            {
                { -2d, -1d, -0.5d },
                { -1d, 1d, -1d },
                { 0d, -2d, -1d }
            });
            Assert.That(m - n, Is.EqualTo(expected));
        }

        [Test]
        public void Scalar_multiplication_works_entry_wise()
        {
            Matrix3D expected = new Matrix3D(new double[,]
            {
                { -0.5d, 0d, 0.25d },
                { 0d, 1d, 0d },
                { 0.5d, -0.5d, 0d }
            });
            Assert.That(0.5d * m, Is.EqualTo(expected));
        }

        [Test]
        public void Matrix_vector_multiplication_produces_correct_result()
        {
            Vector3D v = new Vector3D(3d, 2d, 1d);
            Assert.That(m * v, Is.EqualTo(new Vector3D(-2.5d, 4d, 1d)));
        }
    }
}