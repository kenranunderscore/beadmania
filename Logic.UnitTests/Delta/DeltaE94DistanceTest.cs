using beadmania.Logic.ColorVectors;
using beadmania.Logic.Delta;
using NUnit.Framework;

namespace beadmania.Logic.UnitTests.Delta
{
    [TestFixture]
    public class DeltaE94DistanceTest
    {
        [Test]
        public void Distance_to_zero_is_calculated_correctly()
        {
            LabVector v1 = new LabVector(0d, 0d, 0d);
            LabVector v2 = new LabVector(50d, -50d, 128d);
            DeltaE94Distance distance = new DeltaE94Distance();
            Assert.That(distance.Between(v1, v2), Is.EqualTo(146.23d).Within(0.01d));
        }

        [Test]
        public void Distance_between_two_arbitrary_Lab_vectors_is_calculated_correctly()
        {
            LabVector v1 = new LabVector(100d, 128d, -128d);
            LabVector v2 = new LabVector(0d, 20d, 40d);
            DeltaE94Distance distance = new DeltaE94Distance();
            Assert.That(distance.Between(v1, v2), Is.EqualTo(108.47d).Within(0.01d));
        }
    }
}