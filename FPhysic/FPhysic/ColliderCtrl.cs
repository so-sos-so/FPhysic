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

        private static bool Collider(CapsuleCollider cap1, Rigidbody rigidbody1, CapsuleCollider cap2,
            Rigidbody rigidbody2, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            var normal = cap1.Position - cap2.Position;
            var sqrDis = normal.sqrMagnitude;
            var sqrRadius = FPMath.Pow(cap1.Radius + cap2.Radius, 2);
            if (sqrDis >= sqrRadius) return false;
            normal.Normalize();
            offset = normal * (FPMath.Sqrt(sqrRadius) - FPMath.Sqrt(sqrDis));
            return true;
        }
        
        private static bool Collider(BoxCollider box, Rigidbody rigidbody1, BoxCollider collider2,
            Rigidbody rigidbody2, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            return true;
        }

        private static bool Collider(BoxCollider box, Rigidbody rigidbody1, CapsuleCollider cap2,
            Rigidbody rigidbody2, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            var disOffset = cap2.Position - box.Position;
            var xOffset = FPVector2.Dot(disOffset, box.Right);
            var yOffset = FPVector2.Dot(disOffset, box.Forward);
            var halfSizeX = box.Size.x / 2;
            var halfSizeY = box.Size.y / 2;
            var xMax = halfSizeX + cap2.Radius;
            var yMax = halfSizeY + cap2.Radius;
            if (FPMath.Abs(xOffset) >= xMax || FPMath.Abs(yOffset) >= yMax) return false;
            xOffset = FPMath.Clamp(xOffset, -halfSizeX, halfSizeX);
            yOffset = FPMath.Clamp(yOffset, -halfSizeY, halfSizeY);
            var point = box.Forward * yOffset + box.Right * xOffset + box.Position;
            //需要向外移动的方向
            var capOutDir = cap2.Position - point;
            var len = capOutDir.magnitude;
            //如果point正好在圆心，则需要特殊处理来获得方向
            if (capOutDir == FPVector2.zero)
            {
                capOutDir = cap2.Position - point / 2;
            }
            capOutDir.Normalize();
            offset = capOutDir * (cap2.Radius - len);
            return true;
        }
    }
}