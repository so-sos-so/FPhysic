using FMath;

namespace FPhysic
{
    public interface IEntity
    {
        FPVector3 Position { get; }
        FPVector3 Forward { get; }
        FPVector3 Right { get; }
    }
}