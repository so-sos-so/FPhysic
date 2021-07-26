namespace FMath
{
    public class FPVector4
    {
        public static FPVector4 zero => new FPVector4(0.0f, 0.0f, 0.0f, 0.0f);
        public static FPVector4 one => new FPVector4(1, 1, 1, 1);
        


        public FPInt x;
        public FPInt y;
        public FPInt z;
        public FPInt w;

        public static FPVector4 Cross(FPVector4 lhs, FPVector4 rhs)
        {
            FPInt x = lhs.y * rhs.z - lhs.z * rhs.y;
            FPInt y = lhs.z * rhs.x - lhs.x * rhs.z;
            FPInt z = lhs.x * rhs.y - lhs.y * rhs.x;
            return new FPVector4(x, y, z, 0);
        }

        public static FPInt Dot(FPVector4 v1, FPVector4 v2)
        {
            return (v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w);
        }

        public FPVector4(FPInt x, FPInt y, FPInt z, FPInt w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static FPVector4 operator +(FPVector4 v1, FPVector4 v2)
        {
            return new FPVector4(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
        }

        public static FPVector4 operator -(FPVector4 v1, FPVector4 v2)
        {
            return new FPVector4(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        }

        public static FPVector4 operator *(FPVector4 v1, FPVector4 v2)
        {
            return new FPVector4(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z, v1.w * v2.w);
        }

        public static FPVector4 operator /(FPVector4 v1, FPVector4 v2)
        {
            return new FPVector4(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z, v1.w / v2.w);
        }

        public static FPVector4 operator *(FPVector4 v1, FPInt num)
        {
            return new FPVector4(v1.x * num, v1.y * num, v1.z * num, v1.w * num);
        }

        public static FPVector4 operator /(FPVector4 v1, FPInt num)
        {
            return new FPVector4(v1.x / num, v1.y / num, v1.z / num, v1.w / num);
        }

        public static FPVector4 operator -(FPVector4 v1)
        {
            return new FPVector4(-v1.x, -v1.y, -v1.z, -v1.w);
        }

        public static bool operator ==(FPVector4 v1, FPVector4 v2)
        {
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w;
        }

        public static bool operator !=(FPVector4 v1, FPVector4 v2)
        {
            return !(v1 == v2);
        }

        public static implicit operator FPVector4(FPVector2 v2)
        {
            FPVector4 s = new FPVector4(v2.x, v2.y, 0, 0);
            return s;
        }

        public static implicit operator FPVector4(FPVector3 v2)
        {
            FPVector4 s = new FPVector4(v2.x, v2.y, v2.z, 0);
            return s;
        }

        public static implicit operator FPVector3(FPVector4 v) => new FPVector3(v.x, v.y, v.z);
        
        public bool Equals(FPVector4 other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FPVector4 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                hashCode = (hashCode * 397) ^ w.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"x={x} y={y} z={z} w={w}";
        }
        
        public static FPInt Magnitude(FPVector4 vector) => FPMath.Sqrt(SqrMagnitude(vector));

        public FPInt magnitude => FPMath.Sqrt(sqrMagnitude);
        
        public static FPVector4 Normalize(FPVector4 value)
        {
            var num = Magnitude(value);
            return num > 0 ? value / num : zero;
        }
        
        public FPInt sqrMagnitude => this.x * this.x + this.y * this.y + this.z * this.z + w * w;

        public void Normalize()
        {
            var nor = normalize;
            x = nor.x;
            y = nor.y;
            z = nor.z;
            w = nor.w;
        }
        
        public FPVector4 normalize => Normalize(this);
        
        public static FPInt SqrMagnitude(FPVector4 vector) =>
            vector.x * vector.x + vector.y * vector.y + vector.z * vector.z + vector.w * vector.w;

        /// <summary>
        /// È¡·´
        /// </summary>
        public static FPVector4 opposite(FPVector4 v)
        {
            return new FPVector4(-v.x, -v.y, -v.z, v.w);
        }
    }
}