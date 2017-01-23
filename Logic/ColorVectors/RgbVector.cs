using beadmania.Logic.Math;
using System;

namespace beadmania.Logic.ColorVectors
{
    public class RgbVector : Vector3D
    {
        public RgbVector(double r, double g, double b)
            : base(r, g, b)
        {
            if (r < 0 || g < 0 || b < 0)
                throw new ArgumentOutOfRangeException();
        }
    }
}