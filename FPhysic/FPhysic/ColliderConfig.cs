using FMath;

namespace FPhysic
{
    public class ColliderConfig
    {
        public string Name;
        public ColliderType ColliderType;
        public FPVector3 Position;
        
        //box
        public FPVector3 Size;
        //轴向
        public FPVector3[] Axis;
        //半径
        public FPInt Radius;
    }

    public enum ColliderType
    {
        Box,
        Cylinder,
    }
}