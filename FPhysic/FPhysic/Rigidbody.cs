using FMath;

namespace FPhysic
{
    public class Rigidbody
    {
        public FPInt Mass { get; }
        public Rigidbody(FPInt mass)
        {
            Mass = mass;
        }
    }
}