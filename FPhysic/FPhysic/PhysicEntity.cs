using FMath;

namespace FPhysic
{
    public class PhysicEntity : IEntity
    {
        public ColliderBase Collider { get; }
        public FPVector3 Position { get; set; } = FPVector3.zero;
        public FPVector3 Forward { get; set; } = FPVector3.zero;
        public FPVector3 Right { get; set; } = FPVector3.zero;
        public bool MoveAble { get; }
        public bool IsTrigger { get; }
        public bool HasMoved { get; private set; }

        public PhysicEntity(bool moveAble = false, ColliderBase collider = null, bool trigger = false)
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