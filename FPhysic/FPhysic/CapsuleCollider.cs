using FMath;

namespace FPhysic
{
    public class CapsuleCollider : ColliderBase
    {
        public FPInt Radius { get; }

        public CapsuleCollider(Entity entity, FPVector3 center, FPInt radius) : base(entity, center)
        {
            Radius = radius;
        }
    }
}