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
       
        
        [SetUp]
        public void Setup()
        {
            player = new PhysicEntity();
            player.AddComponent(new CapsuleCollider(player, FPVector2.zero, FPInt.one));
            player.AddComponent(new Rigidbody(1));

            notMoveCollider = new PhysicEntity();
            notMoveCollider.AddComponent(new CapsuleCollider(notMoveCollider, FPVector2.zero, FPInt.one));
            
            moveAbleCollider = new PhysicEntity();
            moveAbleCollider.AddComponent(new CapsuleCollider(moveAbleCollider, FPVector2.zero, FPInt.one));
            moveAbleCollider.AddComponent(new Rigidbody(1));
        }

        [Test]
        public void Test1()
        {
            player.GetComponent<Rigidbody>().Velocity = new FPVector2(0, 1);
            notMoveCollider.Position = new FPVector2(0, 1);
            ColliderCtrl.TryToCollision(player, notMoveCollider);
            Console.WriteLine(player.Position);
        }
    }
}