namespace beadmania.Logic.Math
{
    using System;
    using System.Globalization;

    public class Vector3D : IEquatable<Vector3D>
    {
        private const int Dimensions = 3;
        private const string RepresentationMask = "x={0};y={1};z={2}";
        private readonly double[] coordinates;

        public Vector3D(double x, double y, double z)
        {
            coordinates = new double[] { x, y, z };
        }

        public Vector3D(double[] coordinates)
        {
            if (coordinates.Length != 3)
                throw new ArgumentOutOfRangeException("Array length must be 3");

            this.coordinates = (double[])coordinates.Clone();
        }

        public double X => coordinates[0];

        public double Y => coordinates[1];

        public double Z => coordinates[2];

        public static Vector3D Zero => new Vector3D(0d, 0d, 0d);

        public static Vector3D operator *(double factor, Vector3D v)
        {
            return new Vector3D(factor * v.X, factor * v.Y, factor * v.Z);
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return v1 + (-1d) * v2;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, RepresentationMask, X, Y, Z);
        }

        public bool Equals(Vector3D other)
        {
            if (other == null)
                return false;

            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            var vector = obj as Vector3D;
            return Equals(vector);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 486187739 + X.GetHashCode();
                hash = hash * 486187739 + Y.GetHashCode();
                hash = hash * 486187739 + Z.GetHashCode();
                return hash;
            }
        }
    }
}