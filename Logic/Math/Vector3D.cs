namespace beadmania.Logic.Math
{
    public class Vector3D
    {
        private const int Dimensions = 3;
        private readonly double[] points = new double[Dimensions];

        public Vector3D(double x, double y, double z)
        {
            points[0] = x;
            points[1] = y;
            points[2] = z;
        }

        public double X => points[0];
        public double Y => points[1];
        public double Z => points[2];
    }
}