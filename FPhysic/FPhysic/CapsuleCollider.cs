using FMath;

namespace FPhysic
{
    public class CapsuleCollider : ColliderBase
    {
        public FPInt Radius { get; }

        public CapsuleCollider(FPVector3 center, FPInt radius) : base(center)
        {
            Radius = radius;
        }
    }
}