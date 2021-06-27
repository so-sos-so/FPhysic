using System;

namespace FMath
{
    public struct FPAngle : IEquatable<FPAngle>
    {
        private int value;
        private uint multipler;

        public FPAngle(int value, uint multipler)
        {
            this.value = value;
            this.multipler = multipler;
        }

        public static FPAngle zero = new FPAngle(0, 10000);
        public static FPAngle halfPI = new FPAngle(15708, 10000);
        public static FPAngle PI = new FPAngle(31416, 10000);
        public static FPAngle twoPI = new FPAngle(31416, 10000);
        private static float PIValue = 3.14f;
        
        public static bool operator >(FPAngle a, FPAngle b)
        {
            if(a.multipler == b.multipler) {
                return a.value > b.value;
            }

            throw new Exception("Multipler is unequal.");
        }
        public static bool operator <(FPAngle a, FPAngle b)
        {
            if(a.multipler == b.multipler) {
                return a.value < b.value;
            }

            throw new System.Exception("Multipler is unequal.");
        }
        public static bool operator >=(FPAngle a, FPAngle b)
        {
            if(a.multipler == b.multipler) {
                return a.value >= b.value;
            }

            throw new Exception("Multipler is unequal.");
        }
        public static bool operator <=(FPAngle a, FPAngle b)
        {
            if(a.multipler == b.multipler) {
                return a.value <= b.value;
            }

            throw new Exception("Multipler is unequal.");
        }
        public static bool operator ==(FPAngle a, FPAngle b)
        {
            if(a.multipler == b.multipler) {
                return a.value == b.value;
            }

            throw new Exception("Multipler is unequal.");
        }
        public static bool operator !=(FPAngle a, FPAngle b)
        {
            if(a.multipler == b.multipler) {
                return a.value != b.value;
            }

            throw new Exception("Multipler is unequal.");
        }


        /// <summary>
        /// 转化为视图角度，不可再用于逻辑运算
        /// </summary>
        /// <returns></returns>
        public float Angle => Radian / PIValue * 180;

        /// <summary>
        /// 转化为视图弧度，不可再用于逻辑运算
        /// </summary>
        public float Radian => value * 1.0f / multipler;

        public override bool Equals(object obj) {
            return obj is FPAngle other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked
            {
                return (value * 397) ^ (int) multipler;
            }
        }

        public override string ToString() {
            return $"Value:{value} Multipler:{multipler}";
        }

        public bool Equals(FPAngle other)
        {
            return value == other.value && multipler == other.multipler;
        }
    }
}