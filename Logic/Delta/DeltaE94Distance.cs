using beadmania.Logic.ColorVectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadmania.Logic.Delta
{
    public sealed class DeltaE94Distance
    {
        private const double K1 = 0.045d;
        private const double K2 = 0.015d;

        public double Between(LabVector v1, LabVector v2)
        {
            double a1 = v1.Y;
            double a2 = v2.Y;
            double b1 = v1.Z;
            double b2 = v2.Z;

            double C1 = System.Math.Sqrt(a1 * a1 + b1 * b1);
            double C2 = System.Math.Sqrt(a2 * a2 + b2 * b2);
            double dC = C1 - C2;
            double da = a1 - a2;
            double db = b1 - b2;
            double dL = v1.X - v2.X;
            double dH = System.Math.Sqrt(da * da + db * db - dC * dC);

            double SC = 1 + K1 * C1;
            double SH = 1 + K2 * C1;

            double radicand = dL * dL + System.Math.Pow(dC / SC, 2d) + System.Math.Pow(dH / SH, 2d);
            return System.Math.Sqrt(radicand);
        }
    }
}