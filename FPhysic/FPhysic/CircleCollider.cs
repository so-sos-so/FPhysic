using FMath;

namespace FPhysic
{
    public class CircleCollider : ColliderBase
    {
        public FPInt Radius => radius * FPMath.Max(PhysicEntity.Scale.x.RawFloat, PhysicEntity.Scale.y.RawFloat);
        private FPInt radius;

        public CircleCollider( FPVector2 center, FPInt radius) : base(center)
        {
            this.radius = radius;
        }
    }
}