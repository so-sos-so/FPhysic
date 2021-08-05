using System;
using FMath;
using FPhysic;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private PhysicEntity player;
        private PhysicEntity capCollider;
        private PhysicEntity boxCollider;
       
        
        [SetUp]
        public void Setup()
        {
            player = new PhysicEntity();
            player.AddComponent(new CapsuleCollider(player, FPVector2.zero, FPInt.one / 2));
            player.AddComponent(new Rigidbody());

            capCollider = new PhysicEntity();
            capCollider.AddComponent(new CapsuleCollider(capCollider, FPVector2.zero, 0.5));

            boxCollider = new PhysicEntity();
            boxCollider.AddComponent(new BoxCollider(boxCollider, FPVector2.zero, FPVector2.one));
        }

        [Test]
        public void CapAndBox()
        {
            ResetData();
            player.Position = new FPVector2(0, 0.9);
            boxCollider.Position = new FPVector2(0, 0);
            var collid = ColliderCtrl.TryToCollision(player, boxCollider, out var offset);
            Assert.IsTrue(collid);
            Assert.IsTrue(FPMath.Abs(offset.x - -0.1) <= 0.001);
        }
        
        [Test]
        public void CapAndBoxRotate()
        {
            ResetData();
            //未旋转，能碰撞
            player.Position = new FPVector2(0.75, 0.75);
            boxCollider.Position = new FPVector2(1.5, 1.5);
            var collid = ColliderCtrl.TryToCollision(player, boxCollider, out var offset);
            Assert.IsTrue(collid);
            Console.WriteLine(offset);
            
            //旋转后碰不到
            var s = FPMath.Sin(FPMath.AngleToRadian(45));
            boxCollider.Forward = new FPVector2(s, s);
            boxCollider.Right = new FPVector2(s, -s);
            var collid2 = ColliderCtrl.TryToCollision(player, boxCollider, out var offset2);
            Assert.IsFalse(collid2);
            Assert.AreEqual(offset2, FPVector2.zero);
            Console.WriteLine(offset2);
        }
        
        [Test]
        public void CapAndBoxScale()
        {
            ResetData();
            player.Position = new FPVector2(-0.9f, 0);
            boxCollider.Position = FPVector2.zero;
            boxCollider.Scale = new FPVector2(1, 15);
            var collid2 = ColliderCtrl.TryToCollision(player, boxCollider, out var offset2);
            Assert.IsTrue(collid2);
            Assert.IsTrue(FPMath.Abs(offset2.x - -0.1) <= 0.001);
        }

        [Test]
        public void CapAndCap()
        {
            ResetData();
            player.Position = new FPVector2(0, 0);
            capCollider.Position = new FPVector2(0.5, 0.5);
            var collid = ColliderCtrl.TryToCollision(player, capCollider, out var offset);
            Assert.IsTrue(collid);
            Console.WriteLine(offset);
            
            player.Position = new FPVector2(0, 0);
            capCollider.Position = new FPVector2(1, 1);
            var collid2 = ColliderCtrl.TryToCollision(player, capCollider, out var offset2);
            Assert.IsFalse(collid2);
            Assert.AreEqual(offset2, FPVector2.zero);
        }
        
        [Test]
        public void CapAndCapScale()
        {
            ResetData();
            player.Position = new FPVector2(0, 0);
            capCollider.Position = new FPVector2(1, 1);
            var collid2 = ColliderCtrl.TryToCollision(player, capCollider, out var offset2);
            Assert.IsFalse(collid2);
            Assert.AreEqual(offset2, FPVector2.zero);
            
            player.Position = new FPVector2(0, 0);
            capCollider.Position = new FPVector2(1, 1);
            capCollider.Scale = new FPVector2(2, 2);
            var collid1 = ColliderCtrl.TryToCollision(player, capCollider, out var offset1);
            Assert.IsTrue(collid1);
            Console.WriteLine(offset1);
        }

        private void ResetData()
        {
            player.TransformIdentity();
            capCollider.TransformIdentity();
            boxCollider.TransformIdentity();
        }
    }
}