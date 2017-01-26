using beadmania.Logic.ColorVectors;

namespace beadmania.Logic.ColorSpaceConverters
{
    internal sealed class XyzToLabConverter
    {
        private const double RefX = 95.047d;
        private const double RefY = 100d;
        private const double RefZ = 108.883d;

        public LabVector Convert(XyzVector xyz)
        {
            double x = NormalizeXyzValue(xyz.X / RefX);
            double y = NormalizeXyzValue(xyz.Y / RefY);
            double z = NormalizeXyzValue(xyz.Z / RefZ);

            double L = 116d * y - 16d;
            double a = 500d * (x - y);
            double b = 200d * (y - z);

            return new LabVector(L, a, b);
        }

        private static double NormalizeXyzValue(double value)
        {
            return value > 0.008856d
                ? System.Math.Pow(value, 1d / 3d)
                : 7.787d * value + 16d / 116d;
        }
    }
}