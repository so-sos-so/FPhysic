using FMath;

namespace FPhysic
{
    public class BoxCollider : ColliderBase
    {
        public FPVector2 Size => size * Entity.Scale;
        private FPVector2 size;

        public BoxCollider(Entity entity, FPVector2 center, FPVector2 size) : base(entity, center)
        {
            this.size = size;
        }
    }
}