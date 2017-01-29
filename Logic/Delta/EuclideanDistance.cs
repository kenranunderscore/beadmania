using beadmania.Logic.Math;

namespace beadmania.Logic.Delta
{
    public sealed class EuclideanDistance : IColorDistance<Vector3D>
    {
        public double Between(Vector3D v1, Vector3D v2)
        {
            double radicand = System.Math.Pow(v1.X - v2.X, 2d);
            radicand += System.Math.Pow(v1.Y - v2.Y, 2d);
            radicand += System.Math.Pow(v1.Z - v2.Z, 2d);
            return System.Math.Sqrt(radicand);
        }
    }
}