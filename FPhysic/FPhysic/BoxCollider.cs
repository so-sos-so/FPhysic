using FMath;

namespace FPhysic
{
    public class BoxCollider : ColliderBase
    {
        public FPVector3 Size { get; }

        public BoxCollider(FPVector3 center, FPVector3 size) : base(center)
        {
            Size = size;
        }

        public override bool TryInteraction(ColliderBase other, out FPVector3 result)
        {
            result = FPVector3.zero;
            return true;
        }
    }
}