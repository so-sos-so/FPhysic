using FMath;

namespace FPhysic
{
    public class FBoxCollider : FColliderBase
    {
        public FPVector3 Size { get; }
        //轴向
        public FPVector3[] Axis;

        public FBoxCollider(ColliderConfig config)
        {
            Name = config.Name;
            Position = config.Position;
            Size = config.Size;
            Axis = new[] {config.Axis[0], config.Axis[1], config.Axis[2]};
        }
    }
}