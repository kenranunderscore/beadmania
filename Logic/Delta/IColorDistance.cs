using beadmania.Logic.Math;

namespace beadmania.Logic.Delta
{
    public interface IColorDistance<in T> where T : Vector3D
    {
        double Between(T first, T other);
    }
}