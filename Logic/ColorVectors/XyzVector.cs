using beadmania.Logic.Converters;
using beadmania.Logic.Math;

namespace beadmania.Logic.ColorVectors
{
    public sealed class XyzVector : Vector3D
    {
        public XyzVector(double x, double y, double z)
            : base(x, y, z)
        {
        }

        public LabVector ToLab()
        {
            return new XyzToLabConverter().Convert(this);
        }
    }
}