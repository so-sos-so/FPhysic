using FMath;

namespace FPhysic
{
    public class BoxCollider : ColliderBase
    {
        public FPVector2 Size { get; }

        public BoxCollider(Entity entity, FPVector2 center, FPVector2 size) : base(entity, center)
        {
            Size = size;
        }
    }
}