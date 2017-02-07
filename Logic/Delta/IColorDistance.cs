namespace beadmania.Logic.Delta
{
    using beadmania.Logic.Math;

    public interface IColorDistance<in T> where T : Vector3D
    {
        double Between(T first, T other);
    }
}