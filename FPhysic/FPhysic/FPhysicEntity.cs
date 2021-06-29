using FMath;

namespace FPhysic
{
    public class FPhysicEntity : IEntity
    {
        public FColliderBase Collider { get; }
        public FPVector3 Position { get; set; } = FPVector3.zero;
        public FPVector3 Rotation { get; set; } = FPVector3.zero;
        public bool MoveAble { get; }
        public bool IsTrigger { get; }
        public bool HasMoved { get; private set; }

        public FPhysicEntity(bool moveAble = false, FColliderBase collider = null, bool trigger = false)
        {
            Collider = collider;
            Collider?.SetEntity(this);
            MoveAble = moveAble;
            IsTrigger = trigger;
        }

        public void Move(FPVector3 delta)
        {
            if(delta == FPVector3.zero) return;
            HasMoved = true;
        }
    }
}