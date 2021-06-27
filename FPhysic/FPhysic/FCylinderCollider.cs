using FMath;

namespace FPhysic
{
    public class FCylinderCollider : FColliderBase
    {
        public FPInt Radius;

        public FCylinderCollider(ColliderConfig config)
        {
            Name = config.Name;
            Radius = config.Radius;
        }
        
    }
}