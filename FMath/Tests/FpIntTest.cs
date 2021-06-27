using System;
using FMath;
using NUnit.Framework;

namespace Tests
{
    public class FpIntTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Operation()
        {
            int a = 1;
            FPInt fpInt1 = new FPInt(a);

            float b = 1.2f;
            FPInt fpInt2 = b;
            
            Assert.True(fpInt2 > fpInt1);
            Assert.False(fpInt2 < fpInt1);
            Assert.True(fpInt2 != fpInt1);
            Assert.False(fpInt2 == fpInt1);
            Assert.False(fpInt2.Equals(fpInt1));
            Assert.True(fpInt2 >= fpInt1);
            Assert.False(fpInt2 <= fpInt1);
            Assert.AreEqual(a << 2, (fpInt1 << 2).RawInt);
            Assert.AreEqual(((long) Math.Round(b * 1024) << 2) * 1.0f / 1024, (fpInt2 << 2).RawFloat);

            //由于FPInt是位移10位来计算的，所以可以保证小数点后3位以内是精准的
            Assert.True(Math.Abs(a + b - (fpInt1 + fpInt2).RawFloat) <= 0.001f);
            Assert.True(Math.Abs(a - b - (fpInt1 - fpInt2).RawFloat) <= 0.001f);
            Assert.True(Math.Abs(a * b - (fpInt1 * fpInt2).RawFloat) <= 0.001f);
            Assert.True(Math.Abs(a / b - (fpInt1 / fpInt2).RawFloat) <= 0.001f);
        }

        [Test]
        public void 负数右移动()
        {
            //负数使用位运算，由于计算机底层使用补码来存储负数，右移会产生偏差，计算时可以转化为正数来进行右移位运算
            int hp = 500;
            var val1 = hp * new FPInt(0.3f);
            var val2 = hp * new FPInt(-0.3f);
            
            Assert.AreNotEqual(Math.Abs(3 >> 1), Math.Abs((-3) >> 1));
            Assert.AreEqual(Math.Abs(2 >> 1), Math.Abs((-2) >> 1));
            
            Assert.AreEqual(Math.Abs(val1.RawInt), Math.Abs(val2.RawInt));
        }

        [Test]
        public void Program()
        {
            Console.WriteLine(FPMath.Sqrt(3));
            Console.WriteLine(FPMath.Sqrt(11617));
        }
    }
}