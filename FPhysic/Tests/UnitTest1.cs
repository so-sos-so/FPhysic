using System;
using System.Collections.Generic;
using FMath;
using FPhysic;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private PhysicEntity player;
        private PhysicEntity notMoveCollider;
        private PhysicEntity moveAbleCollider;
        private PhysicEntity boxCollider;
       
        
        [SetUp]
        public void Setup()
        {
            player = new PhysicEntity();
            player.AddComponent(new CapsuleCollider(player, FPVector2.zero, FPInt.one / 2));
            player.AddComponent(new Rigidbody());

            notMoveCollider = new PhysicEntity();
            notMoveCollider.AddComponent(new CapsuleCollider(notMoveCollider, FPVector2.zero, FPInt.one));
            
            moveAbleCollider = new PhysicEntity();
            moveAbleCollider.AddComponent(new CapsuleCollider(moveAbleCollider, FPVector2.zero, FPInt.one));
            moveAbleCollider.AddComponent(new Rigidbody());

            boxCollider = new PhysicEntity();
            boxCollider.AddComponent(new BoxCollider(boxCollider, FPVector2.zero, FPVector2.one));
        }

        [Test]
        public void Test1()
        {
            //未旋转，能碰撞
            player.Position = new FPVector2(0.75, 0.75);
            player.GetComponent<Rigidbody>().Velocity = new FPVector2(0, 1);
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
    }
}