using FMath;

namespace FPhysic
{
    public class FBoxCollider : FColliderBase
    {
        public FPVector3 Size { get; }

        public FBoxCollider(FPVector3 center, FPVector3 size) : base(center)
        {
            Size = size;
        }

        public override bool TryInteraction(FColliderBase other, out FPVector3 result)
        {
            result = FPVector3.zero;
            return true;
        }
    }
}