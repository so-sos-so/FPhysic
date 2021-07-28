using FMath;

namespace FPhysic
{
    public class Rigidbody
    {
        public FPInt Mass { get; }
        public FPVector2 Velocity { get; set; }
        public Rigidbody(FPInt mass)
        {
            Mass = mass;
        }
    }
}