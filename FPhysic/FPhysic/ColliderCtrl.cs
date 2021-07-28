using System;
using FMath;

namespace FPhysic
{
    public class ColliderCtrl
    {
        public static bool TryToCollision(Entity entity1, Entity entity2)
        {
            var rig1 = entity1.GetComponent<Rigidbody>();
            var rig2 = entity2.GetComponent<Rigidbody>();
            if (rig1 == null && rig2 == null) return false;
            var collider1 = entity1.GetComponent<ColliderBase>();
            var collider2 = entity2.GetComponent<ColliderBase>();
            if (collider1 == null || collider2 == null) return false;
            if (!ColliderBase.Intersects(collider1, collider2)) return false;
            switch (collider1)
            {
                case CapsuleCollider capsuleCollider when collider2 is CapsuleCollider capsuleCollider2:
                    Collider(capsuleCollider, rig1, capsuleCollider2, rig2);
                    break;
                case CapsuleCollider capsuleCollider:
                {
                    if (collider2 is BoxCollider boxCollider)
                    {
                        Collider(boxCollider, rig2, capsuleCollider, rig1);
                    }

                    break;
                }
                case BoxCollider boxCollider when collider2 is CapsuleCollider capsuleCollider2:
                    Collider(boxCollider,rig1, capsuleCollider2,rig2);
                    break;
                case BoxCollider boxCollider:
                {
                    if (collider2 is BoxCollider boxCollider2)
                    {
                        Collider(boxCollider,rig1, boxCollider2,rig2);
                    }

                    break;
                }
            }

            return true;
        }

        private static void Collider(CapsuleCollider collider1, Rigidbody rigidbody1, CapsuleCollider collider2,
            Rigidbody rigidbody2)
        {
            var normal = (collider2.Position - collider1.Position).normalize;
            var tan = new FPVector2(-normal.y, normal.x);
            FPInt v1N = FPInt.zero;
            FPInt v2N = FPInt.zero;
            FPInt v1T = FPInt.zero;
            FPInt v2T = FPInt.zero;
            
            if (rigidbody1 != null)
            {
                v1N = FPVector2.Dot(rigidbody1.Velocity, normal);
                v1T = FPVector2.Dot(rigidbody1.Velocity, tan);
            }
            if (rigidbody2 != null)
            {
                v2N = FPVector2.Dot(rigidbody2.Velocity, normal);
                v2T = FPVector2.Dot(rigidbody2.Velocity, tan);
            }
            FPInt v1NAfter = v1N;
            FPInt v2NAfter = v2N;
            if (rigidbody1 != null && rigidbody2 != null)
            {
                v1NAfter = (v1N * (rigidbody1.Mass - rigidbody2.Mass) + 2 * rigidbody2.Mass * v2N) /
                           (rigidbody1.Mass + rigidbody2.Mass);
                v2NAfter = (v2N * (rigidbody2.Mass - rigidbody1.Mass) + 2 * rigidbody1.Mass * v2N) /
                           (rigidbody1.Mass + rigidbody2.Mass);
            }
            
            //v1nAfter 和 v2nAfter 分别是两小球碰撞后的速度，现在可以先判断一下，如果 v1nAfter 小于 v2nAfter，那么第 1 个小球和第 2 个小球会越来越远，此时不用处理碰撞
            if(v1NAfter < v2NAfter) return;

            if (rigidbody1 != null)
            {
                var v1VectorNorm = normal * v1NAfter;
                var v1VectorTan = tan * v1T;
                var velocity1After = v1VectorNorm * v1VectorTan;
                rigidbody1.Velocity = velocity1After;
            }

            if (rigidbody2 != null)
            {
                var v2VectorNorm = normal * v2NAfter;
                var v2VectorTan = tan * v2T;
                var velocity2After = v2VectorNorm * v2VectorTan;
                rigidbody2.Velocity = velocity2After;
            }
        }
        
        private static void Collider(BoxCollider collider1, Rigidbody rigidbody1, BoxCollider collider2,
            Rigidbody rigidbody2)
        {
            
        }
        
        private static void Collider(BoxCollider collider1, Rigidbody rigidbody1, CapsuleCollider collider2,
            Rigidbody rigidbody2)
        {
            
        }
    }
}