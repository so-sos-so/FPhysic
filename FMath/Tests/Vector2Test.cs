using System;
using FMath;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class Vector2Test
    {
        [Test]
        public void Operation()
        {
            FPVector2 vec1 = new FPVector2(1, 1);
            FPVector2 vec2 = new FPVector2(1.1f, 1.1f);
            FPVector2 vec3 = new FPVector2(2, 2);

            
            Assert.True(vec1 != vec2);
            Assert.True(vec1.sqrMagnitude < vec2.sqrMagnitude);
            Assert.True(vec1.magnitude < vec2.magnitude);
            Assert.False(vec1 == vec2);
            Assert.False(vec1.Equals(vec2));

            Assert.AreEqual(new FPInt(8), vec3.sqrMagnitude);
            Assert.AreEqual(FPMath.Sqrt(8), vec3.magnitude);

            Assert.AreEqual(vec1, vec3 / 2);
            Assert.AreEqual(vec3, vec1 * 2);

            Assert.AreEqual(1 / vec1.magnitude, vec1.normalize.x);
           
            Assert.AreEqual(vec1.normalize, FPVector2.Normalize(vec1));

            Assert.True(Math.Abs(Vector2.Angle(Vector2.one, new Vector2(1, 2))- FPVector2.Angle(FPVector2.one, new FPVector2(1, 2)).Angle) <= 1);
            
            Assert.AreEqual(Vector2.Dot(Vector2.one, new Vector2(1, 2)), FPVector2.Dot(FPVector2.one, new FPVector2(1, 2)).RawFloat);

        }
    }
}