using System;
using UnityEngine;

namespace FMath
{
    public struct FPVector3 : IEquatable<FPVector3>
    {
        public FPInt x;
        public FPInt y;
        public FPInt z;

        public FPVector3(FPInt x, FPInt y, FPInt z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public FPVector3(UnityEngine.Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }

        public UnityEngine.Vector3 Vector3 => new UnityEngine.Vector3(x.RawFloat, y.RawFloat, z.RawFloat);

        #region 常用向量

        public static FPVector3 zero => new FPVector3(0, 0, 0);
        public static FPVector3 one => new FPVector3(1, 1, 1);
        public static FPVector3 forward => new FPVector3(0, 0, 1);
        public static FPVector3 back => new FPVector3(0, 0, -1);
        public static FPVector3 left => new FPVector3(-1, 0, 0);
        public static FPVector3 right => new FPVector3(1, 0, 0);
        public static FPVector3 up => new FPVector3(0, 1, 0);
        public static FPVector3 down => new FPVector3(0, -1, 0);

        #endregion

        #region 运算符

        public static FPVector3 operator +(FPVector3 a, FPVector3 b) => new FPVector3(a.x + b.x, a.y + b.y, a.z + b.z);

        public static FPVector3 operator -(FPVector3 a, FPVector3 b) => new FPVector3(a.x - b.x, a.y - b.y, a.z - b.z);

        public static FPVector3 operator -(FPVector3 a) => new FPVector3(-a.x, -a.y, -a.z);

        public static FPVector3 operator *(FPVector3 a, FPInt d) => new FPVector3(a.x * d, a.y * d, a.z * d);

        public static FPVector3 operator *(FPInt d, FPVector3 a) => new FPVector3(a.x * d, a.y * d, a.z * d);

        public static FPVector3 operator /(FPVector3 a, FPInt d) => new FPVector3(a.x / d, a.y / d, a.z / d);

        public static bool operator ==(FPVector3 v1, FPVector3 v2)
        {
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }

        public static bool operator !=(FPVector3 v1, FPVector3 v2) => !(v1 == v2);

        public static FPInt Magnitude(FPVector3 vector) => FPMath.Sqrt(SqrMagnitude(vector));

        public FPInt magnitude => FPMath.Sqrt(sqrMagnitude);

        public static FPInt SqrMagnitude(FPVector3 vector) =>
            vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;

        public FPInt sqrMagnitude => this.x * this.x + this.y * this.y + this.z * this.z;

        public static FPVector3 Normalize(FPVector3 value)
        {
            var num = Magnitude(value);
            return num > 0 ? value / num : zero;
        }

        public void Normalize()
        {
            var nor = normalize;
            x = nor.x;
            y = nor.y;
            z = nor.z;
        }

        public FPVector3 normalize => Normalize(this);
        
        public static FPInt Dot(FPVector3 v1, FPVector3 v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public static FPVector3 Cross(FPVector3 v1, FPVector3 v2)
        {
            var result = new FPVector3(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
            return result;
        }
        
        public static FPAngle Angle(FPVector3 from, FPVector3 to)
        {
            var num =  FPMath.Sqrt(from.sqrMagnitude *  to.sqrMagnitude);
            var dot = Dot(from, to);
            if (num == 0) return FPAngle.zero;
            FPInt val = dot / num;
            return FPMath.Acos(val);
        }
        
        public static implicit operator FPVector3(Vector3 val)
        {
            return new FPVector3(val);
        }

        #endregion

        public long[] LongArray => new[] {x.ScaledValue, y.ScaledValue, z.ScaledValue};

        public override string ToString()
        {
            return $"x:{x} y:{y} z:{z}";
        }

        public bool Equals(FPVector3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        public override bool Equals(object obj)
        {
            return obj is FPVector3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                return hashCode;
            }
        }
    }
}