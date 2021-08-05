using FMath;

namespace FPhysic
{
    public class BoxCollider : ColliderBase
    {
        public FPVector2 Size => size * PhysicEntity.Scale;
        private FPVector2 size;

        public BoxCollider(FPVector2 center, FPVector2 size) : base(center)
        {
            this.size = size;
        }
    }
}