using FMath;

namespace FPhysic
{
    public class CapsuleCollider : ColliderBase
    {
        public FPInt Radius { get; }

        public CapsuleCollider(Entity entity, FPVector2 center, FPInt radius) : base(entity, center)
        {
            Radius = radius;
        }
    }
}