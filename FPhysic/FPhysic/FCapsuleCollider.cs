using FMath;

namespace FPhysic
{
    public class FCapsuleCollider : FColliderBase
    {
        public FPInt Radius { get; }

        public FCapsuleCollider(FPVector3 center, FPInt radius) : base(center)
        {
            Radius = radius;
        }
    }
}