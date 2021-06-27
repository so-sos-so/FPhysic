using System;
using System.Globalization;

namespace FMath
{
    public struct FPInt : IEquatable<FPInt>
    {
        private long scaledValue;
        internal long ScaledValue => scaledValue;
        public float RawFloat => scaledValue * 1.0f / MUL_FACTOR;
        public int RawInt
        {
            get
            {
                if (scaledValue >= 0)
                    return (int) (scaledValue >> BIT_SCALE_Move);
                return -(int) (-scaledValue >> BIT_SCALE_Move);
            }
        }

        public static readonly FPInt zero = new FPInt(0);
        public static readonly FPInt one = new FPInt(1);

        //由于FPInt是位移10位来计算的，所以可以保证小数点后3位以内是精准的
        //位移的位数 2^10
        private const int BIT_SCALE_Move = 10;
        private const long MUL_FACTOR = 1 << BIT_SCALE_Move;
        
        #region 构造函数
        public FPInt(int val)
        {
            scaledValue = val << BIT_SCALE_Move;
        }

        /// <summary>
        /// 内部使用，已经缩放的数据
        /// </summary>
        /// <param name="scaledValue"></param>
        private FPInt(long scaledValue)
        {
            this.scaledValue = scaledValue;
        }
        
        public FPInt(float val)
        {
            scaledValue = (long) Math.Round(val * MUL_FACTOR);
        }
        #endregion

        #region 操作符

        public static FPInt operator +(FPInt val, FPInt val2)
        {
            return new FPInt(val.scaledValue + val2.scaledValue);
        }
        
        public static FPInt operator -(FPInt val, FPInt val2)
        {
            return new FPInt(val.scaledValue - val2.scaledValue);
        }
        
        public static FPInt operator *(FPInt val, FPInt val2)
        {
            long value = val.scaledValue * val2.scaledValue;
            if (value >= 0)
                value >>= BIT_SCALE_Move;
            else
                value = -(-value >> BIT_SCALE_Move);
            return new FPInt(value);
        }
        
        public static FPInt operator /(FPInt val, FPInt val2)
        {
            if (val2.scaledValue == 0)
            {
                throw new DivideByZeroException();
            }
            
            return new FPInt((val.scaledValue << BIT_SCALE_Move) / val2.scaledValue);
        }
       
        public static FPInt operator -(FPInt val)
        {
            return new FPInt(-val.scaledValue);
        }
        
        public static bool operator ==(FPInt val1,FPInt val2)
        {
            return val1.Equals(val2);
        }

        public static bool operator !=(FPInt val1, FPInt val2)
        {
            return !(val1 == val2);
        }
        
        public static bool operator >=(FPInt val1,FPInt val2)
        {
            return val1.Equals(val2) || val1 > val2;
        }

        public static bool operator <=(FPInt val1, FPInt val2)
        {
            return val1.Equals(val2) || val1 < val2;
        }

        public static bool operator >(FPInt val1, FPInt val2)
        {
            return val1.scaledValue > val2.scaledValue;
        }

        public static bool operator <(FPInt val1, FPInt val2)
        {
            return val1.scaledValue < val2.scaledValue;
        }

        public static FPInt operator <<(FPInt val, int moveCount)
        {
            return new FPInt(val.scaledValue << moveCount);
        }
        
        public static FPInt operator >>(FPInt val, int moveCount)
        {
            long value;
            if (val.scaledValue >= 0)
                value = val.scaledValue >>= moveCount;
            else
                value = -(-val.scaledValue >> moveCount);
            return new FPInt(value);
        }

        public static implicit operator FPInt(int val)
        {
            return new FPInt(val);
        }
        
        public static implicit operator FPInt(float val)
        {
            return new FPInt(val);
        }

        public bool Equals(FPInt other)
        {
            return scaledValue == other.scaledValue;
        }

        public override bool Equals(object obj)
        {
            return obj is FPInt other && Equals(other);
        }

        public override int GetHashCode()
        {
            return scaledValue.GetHashCode();
        }
        #endregion

        public override string ToString()
        {
            return RawFloat.ToString(CultureInfo.InvariantCulture);
        }
    }
}