using System;

namespace FMath
{
    public static class FPMath
    {
        public static FPInt Sqrt(FPInt val, int iteratorCount = 8)
        {
            if (val == FPInt.zero)
                return 0;
            if (val < FPInt.zero)
                throw new DivideByZeroException();
            FPInt result = val;
            for (int i = 0; i < iteratorCount; i++)
            {
                var lastResult = result;
                result = (result + val / result) >> 1;
                if(lastResult == result)
                    break;
            }
            return result;
        }

        public static FPInt Cos(FPInt rad)
        {
            return Math.Cos(rad.RawFloat);
        }
        
        public static FPInt Sin(FPInt rad)
        {
            return Math.Sin(rad.RawFloat);
        }
        
        public static FPInt Abs(FPInt val)
        {
            return val > 0 ? val : -val;
        }
        
        public static FPAngle Acos(FPInt val)
        {
            FPInt rate = val * AcosTable.HalfIndexCount + AcosTable.HalfIndexCount;
            rate = Clamp(rate, FPInt.zero, AcosTable.IndexCount);
            int rad = AcosTable.table[rate.RawInt];
            return new FPAngle(rad, AcosTable.Multipler);
        }

        public static FPInt Clamp(FPInt value, FPInt min, FPInt max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        public static FPInt Pow(FPInt x, FPInt y)
        {
            FPInt result = 1;
            for (int i = 0; i < y; i++)
            {
                result *= x;
            }

            return result;
        }
    }
}