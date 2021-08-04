using System;
using FMath;

namespace FPhysic
{
    public class ColliderCtrl
    {
        public static bool TryToCollision(Entity entity1, Entity entity2, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            var rig1 = entity1.GetComponent<Rigidbody>();
            var rig2 = entity2.GetComponent<Rigidbody>();
            if (rig1 == null && rig2 == null) return false;
            var collider1 = entity1.GetComponent<ColliderBase>();
            var collider2 = entity2.GetComponent<ColliderBase>();
            if (collider1 == null || collider2 == null) return false;
            switch (collider1)
            {
                case CapsuleCollider capsuleCollider when collider2 is CapsuleCollider capsuleCollider2:
                    return Collider(capsuleCollider, rig1, capsuleCollider2, rig2, out offset);
                case CapsuleCollider capsuleCollider:
                {
                    if (collider2 is BoxCollider boxCollider)
                    {
                        return Collider(boxCollider, rig2, capsuleCollider, rig1, out offset);
                    }

                    break;
                }
                case BoxCollider boxCollider when collider2 is CapsuleCollider capsuleCollider2:
                    return Collider(boxCollider, rig1, capsuleCollider2, rig2, out offset);
                    break;
                case BoxCollider boxCollider:
                {
                    if (collider2 is BoxCollider boxCollider2)
                    {
                        return Collider(boxCollider,rig1, boxCollider2,rig2, out offset);
                    }

                    break;
                }
            }

            return true;
        }

        private static bool Collider(CapsuleCollider collider1, Rigidbody rigidbody1, CapsuleCollider cap,
            Rigidbody rigidbody2, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            var normal = (cap.Position - collider1.Position).normalize;
            var tan = new FPVector2(-normal.y, normal.x);
            FPInt v1N = FPInt.zero;
            FPInt v2N = FPInt.zero;
            FPInt v1T = FPInt.zero;
            FPInt v2T = FPInt.zero;
            return true;
        }
        
        private static bool Collider(BoxCollider box, Rigidbody rigidbody1, BoxCollider collider2,
            Rigidbody rigidbody2, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            return true;
        }

        private static bool Collider(BoxCollider box, Rigidbody rigidbody1, CapsuleCollider cap,
            Rigidbody rigidbody2, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            var disOffset = cap.Position - box.Position;
            var xOffset = FPVector2.Dot(disOffset, box.Right);
            var yOffset = FPVector2.Dot(disOffset, box.Forward);
            var halfSizeX = box.Size.x / 2;
            var halfSizeY = box.Size.y / 2;
            var xMax = halfSizeX + cap.Radius;
            var yMax = halfSizeY + cap.Radius;
            if (FPMath.Abs(xOffset) >= xMax || FPMath.Abs(yOffset) >= yMax) return false;
            xOffset = FPMath.Clamp(xOffset, -halfSizeX, halfSizeX);
            yOffset = FPMath.Clamp(xOffset, -halfSizeY, halfSizeY);
            var point = box.Forward * yOffset + box.Right * xOffset + box.Position;
            //需要向外移动的方向
            var capOutDir = point - cap.Position;
            var len = capOutDir.magnitude;
            //如果point正好在圆心，则需要特殊处理来获得方向
            if (capOutDir == FPVector2.zero)
            {
                capOutDir = cap.Position - point / 2;
            }
            capOutDir.Normalize();
            offset = capOutDir * (cap.Radius - len);
            return true;
        }
    }
}