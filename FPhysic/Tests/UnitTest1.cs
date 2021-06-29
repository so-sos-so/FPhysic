using System.Collections.Generic;
using FMath;
using FPhysic;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private FPhysicEntity player;
        private List<FPhysicEntity> colliders;
        
        [SetUp]
        public void Setup()
        {
            player = new FPhysicEntity(moveAble: true, collider: FColliderBase.Create(FPVector3.zero,  0.5f));
            colliders = new List<FPhysicEntity>()
            {
                new FPhysicEntity(collider: FColliderBase.Create(FPVector3.zero, new FPVector3(17, 1, 1)))
                    {Position = new FPVector3(2, 0, 0)},
                new FPhysicEntity(collider: FColliderBase.Create(FPVector3.zero, new FPVector3(10, 1, 1)))
                    {Position = new FPVector3(0, 0, 10)},
                new FPhysicEntity(collider: FColliderBase.Create(FPVector3.zero, new FPVector3(10, 1, 1)))
                    {Position = new FPVector3(-7, 0, -7), Rotation = new FPVector3(0, 60, 0)},
                new FPhysicEntity(collider: FColliderBase.Create(FPVector3.zero, new FPVector3(10, 1, 1)))
                    {Position = new FPVector3(-7, 0, -2), Rotation = new FPVector3(0, 120, 0)},
                new FPhysicEntity(collider: FColliderBase.Create(FPVector3.zero, new FPVector3(15, 1, 1)))
                    {Position = new FPVector3(7, 0, -5), Rotation = new FPVector3(0, 120, 0)},
                new FPhysicEntity(collider: FColliderBase.Create(FPVector3.zero, new FPVector3(10, 1, 1)),
                    trigger: true) {Position = new FPVector3(0, 0, -5)},
                new FPhysicEntity(collider: FColliderBase.Create(FPVector3.zero, 2)) {Position = new FPVector3(0, 0, -2)},
            };
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}