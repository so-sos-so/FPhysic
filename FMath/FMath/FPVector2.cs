using System;
using UnityEngine;

namespace FMath
{
    public class FPVector2 : IEquatable<FPVector2>
    {
        public FPInt x;
        public FPInt y;

        public FPVector2(FPInt x, FPInt y)
        {
            this.x = x;
            this.y = y;
        }
        
        public FPVector2(UnityEngine.Vector2 vector2)
        {
            x = vector2.x;
            y = vector2.y;
        }

        public UnityEngine.Vector2 Vector2 => new UnityEngine.Vector3(x.RawFloat, y.RawFloat);

        #region 常用向量

        public static FPVector2 zero => new FPVector2(0, 0);
        public static FPVector2 one => new FPVector2(1, 1);

        #endregion

        #region 运算符

        public static FPVector2 operator +(FPVector2 a, FPVector2 b) => new FPVector2(a.x + b.x, a.y + b.y);

        public static FPVector2 operator -(FPVector2 a, FPVector2 b) => new FPVector2(a.x - b.x, a.y - b.y);

        public static FPVector2 operator -(FPVector2 a) => new FPVector2(-a.x, -a.y);

        public static FPVector2 operator *(FPVector2 a, FPInt d) => new FPVector2(a.x * d, a.y * d);

        public static FPVector2 operator *(FPInt d, FPVector2 a) => new FPVector2(a.x * d, a.y * d);

        public static FPVector2 operator /(FPVector2 a, FPInt d) => new FPVector2(a.x / d, a.y / d);

        public static bool operator ==(FPVector2 v1, FPVector2 v2)
        {
            return v1.x == v2.x && v1.y == v2.y;
        }

        public static bool operator !=(FPVector2 v1, FPVector2 v2) => !(v1 == v2);

        public static FPInt Magnitude(FPVector2 vector) => FPMath.Sqrt(SqrMagnitude(vector));

        public FPInt magnitude => FPMath.Sqrt(sqrMagnitude);

        public static FPInt SqrMagnitude(FPVector2 vector) =>
            vector.x * vector.x + vector.y * vector.y;

        public FPInt sqrMagnitude => this.x * this.x + this.y * this.y;

        public static FPVector2 Normalize(FPVector2 value)
        {
            var num = Magnitude(value);
            return num > 0 ? value / num : zero;
        }

        public void Normalize()
        {
            var nor = normalize;
            x = nor.x;
            y = nor.y;
        }

        public FPVector2 normalize => Normalize(this);

        public static FPInt Dot(FPVector2 v1, FPVector2 v2)
        {
            return v1.x * v2.x + v1.y * v2.y;
        }

        public static FPAngle Angle(FPVector2 from, FPVector2 to)
        {
            var num =  FPMath.Sqrt(from.sqrMagnitude *  to.sqrMagnitude);
            var dot = Dot(from, to);
            if (num == 0) return FPAngle.zero;
            FPInt val = dot / num;
            return FPMath.Acos(val);
        }

        public static FPInt Distance(FPVector2 v1, FPVector2 v2)
        {
            return FPMath.Sqrt(SqrDistance(v1, v2));
        }
        
        public static FPInt SqrDistance(FPVector2 v1, FPVector2 v2)
        {
            return FPMath.Pow((v1.x - v2.x), 2) + FPMath.Pow((v1.y - v2.y), 2);
        }
        
        public static implicit operator FPVector2(Vector2 val)
        {
            return new FPVector2(val);
        }

        #endregion

        public long[] LongArray => new[] {x.ScaledValue, y.ScaledValue};

        public override string ToString()
        {
            return $"x:{x} y:{y}";
        }

        public bool Equals(FPVector2 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FPVector2) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }
    }
}