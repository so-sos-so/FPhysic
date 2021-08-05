using FMath;

namespace FPhysic
{
    public class CapsuleCollider : ColliderBase
    {
        public FPInt Radius => radius * FPMath.Max(Entity.Scale.x.RawFloat, Entity.Scale.y.RawFloat);
        private FPInt radius;

        public CapsuleCollider(Entity entity, FPVector2 center, FPInt radius) : base(entity, center)
        {
            this.radius = radius;
        }
    }
}