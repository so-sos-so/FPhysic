using FMath;

namespace FPhysic
{
    public class PhysicEntity : Entity
    {
        public ColliderBase Collider { get; private set; }
        public bool MoveAble { get; }
        public bool IsTrigger { get; }
        public bool HasMoved { get; private set; }

        public PhysicEntity(bool moveAble = false, bool trigger = false)
        {
            MoveAble = moveAble;
            IsTrigger = trigger;
        }

        public void AddCollider(ColliderBase collider)
        {
            Collider = collider;
        }

        public void Move(FPVector3 delta)
        {
            if(delta == FPVector3.zero) return;
            HasMoved = true;
        }
    }
}