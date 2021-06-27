using System;
using FMath;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class Vector3Test
    {
        [Test]
        public void Operation()
        {
            FPVector3 vec1 = new FPVector3(1, 1, 1);
            FPVector3 vec2 = new FPVector3(1.1f, 1.1f, 1.1f);
            FPVector3 vec3 = new FPVector3(2, 2, 2);

            
            Assert.True(vec1 != vec2);
            Assert.True(vec1.sqrMagnitude < vec2.sqrMagnitude);
            Assert.True(vec1.magnitude < vec2.magnitude);
            Assert.False(vec1 == vec2);
            Assert.False(vec1.Equals(vec2));

            Assert.AreEqual(new FPInt(12), vec3.sqrMagnitude);
            Assert.AreEqual(FPMath.Sqrt(12), vec3.magnitude);

            Assert.AreEqual(vec1, vec3 / 2);
            Assert.AreEqual(vec3, vec1 * 2);

            Assert.AreEqual(1 / vec1.magnitude, vec1.normalize.x);
           
            Assert.AreEqual(vec1.normalize, FPVector3.Normalize(vec1));

            //Assert.AreEqual(90, FPVector3.Angle(FPVector3.up, FPVector3.right).Angle);
            Assert.True(Math.Abs(Vector3.Angle(Vector3.one, new Vector3(1, 2, 3))- FPVector3.Angle(FPVector3.one, new FPVector3(1, 2, 3)).Angle) <= 0.1f);
            
            Assert.AreEqual(Vector3.Dot(Vector3.one, new Vector3(1, 2, 3)),
                FPVector3.Dot(FPVector3.one, new FPVector3(1, 2, 3)).RawFloat);

            Assert.AreEqual(Vector3.Cross(Vector3.one, new Vector3(1, 2, 3)),FPVector3.Cross(FPVector3.one, new FPVector3(1, 2, 3)).Vector3);
            
        }
    }
}