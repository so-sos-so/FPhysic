using System.Collections.Generic;
using FMath;
using FPhysic;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private PhysicEntity player;
        private List<PhysicEntity> colliders;
        
        [SetUp]
        public void Setup()
        {
            player = new PhysicEntity(moveAble: true, collider: ColliderBase.Create(FPVector3.zero,  0.5f));
            colliders = new List<PhysicEntity>()
            {
                //0 cube
                new PhysicEntity(collider: ColliderBase.Create(FPVector3.zero, new FPVector3(17, 1, 1)))
                    {Position = new FPVector3(2, 0, 0)},
                //1 cube 1
                new PhysicEntity(collider: ColliderBase.Create(FPVector3.zero, new FPVector3(10, 1, 1)))
                    {Position = new FPVector3(0, 0, -10)},
                //2 cube 2
                new PhysicEntity(collider: ColliderBase.Create(FPVector3.zero, new FPVector3(10, 1, 1)))
                    {Position = new FPVector3(-7, 0, -7), Forward = new FPVector3(0.866f, 0, 0.5f), Right = new FPVector3(0.5f,0,-0.866f)},
                //3 cube 3
                new PhysicEntity(collider: ColliderBase.Create(FPVector3.zero, new FPVector3(10, 1, 1)))
                    {Position = new FPVector3(-7, 0, -2), Forward = new FPVector3(0.866f, 0, -0.5f), Right = new FPVector3(-0.5f,0,-0.866f)},
                //4 cube 4
                new PhysicEntity(collider: ColliderBase.Create(FPVector3.zero, new FPVector3(15, 1, 1)))
                    {Position = new FPVector3(7, 0, -5), Forward = new FPVector3(0.866f, 0, -0.5f), Right = new FPVector3(-0.5f,0,-0.866f)},
                //5 cube 5
                new PhysicEntity(collider: ColliderBase.Create(FPVector3.zero, new FPVector3(10, 1, 1)),
                    trigger: true) {Position = new FPVector3(0, 0, -5)},
                //6 Capsule
                new PhysicEntity(collider: ColliderBase.Create(FPVector3.zero, 2)) {Position = new FPVector3(0, 0, -2)},
            };
        }

        [Test]
        public void Test1()
        {
            player.Position = new FPVector3(-2, 0, -4.5f);
            var collision = player.Collider.TryInteraction(colliders[5].Collider, out var result);
            Assert.True(collision);
            Assert.AreEqual(result, new FPVector3(0,0, 0.5f));
        }
    }
}