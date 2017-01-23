using beadmania.Logic.Math;
using System;

namespace beadmania.Logic.ColorVectors
{
    public class RgbVector : Vector3D
    {
        private const double MinValue = 0d;
        private const double MaxValue = 255d;

        public RgbVector(double r, double g, double b)
            : base(r, g, b)
        {
            if (IsOutOfRange(r) || IsOutOfRange(g) || IsOutOfRange(b))
                throw new ArgumentOutOfRangeException();
        }

        private static bool IsOutOfRange(double val) => val < MinValue || val > MaxValue;
    }
}