namespace beadmania.Logic.UnitTests.Delta
{
    using beadmania.Logic.Delta;
    using beadmania.Logic.Math;
    using NUnit.Framework;

    [TestFixture]
    public class EuclideanDistanceTest
    {
        [Test]
        public void Vector_with_only_x_coordinate_has_Euclidean_distance_x_to_zero()
        {
            Vector3D v = new Vector3D(11d, 0d, 0d);
            EuclideanDistance distance = new EuclideanDistance();
            Assert.That(distance.Between(v, Vector3D.Zero), Is.EqualTo(11d));
        }

        [Test]
        public void Vector_with_only_negative_y_coordinate_has_Euclidean_distance_abs_y_to_zero()
        {
            Vector3D v = new Vector3D(0d, -3d, 0d);
            EuclideanDistance distance = new EuclideanDistance();
            Assert.That(distance.Between(v, Vector3D.Zero), Is.EqualTo(3d));
        }

        [Test]
        public void Vector_with_only_z_coordinate_has_Euclidean_distance_z_to_zero()
        {
            Vector3D v = new Vector3D(0d, 0d, 1000d);
            EuclideanDistance distance = new EuclideanDistance();
            Assert.That(distance.Between(v, Vector3D.Zero), Is.EqualTo(1000d));
        }

        [Test]
        public void Distance_between_two_arbitrary_vectors_is_calculated_correctly()
        {
            Vector3D v1 = new Vector3D(-4d, 0d, 1d);
            Vector3D v2 = new Vector3D(0d, 4d, -10d);
            EuclideanDistance distance = new EuclideanDistance();
            Assert.That(distance.Between(v1, v2), Is.EqualTo(System.Math.Sqrt(153d)));
        }
    }
}