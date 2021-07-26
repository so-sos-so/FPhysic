using FMath;

namespace FPhysic
{
    public class BoxCollider : ColliderBase
    {
        public FPVector3 Size { get; }

        public BoxCollider(Entity entity, FPVector3 center, FPVector3 size) : base(entity, center)
        {
            Size = size;
        }
    }
}