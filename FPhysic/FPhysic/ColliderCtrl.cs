using System;
using FMath;

namespace FPhysic
{
    public class ColliderCtrl
    {
        /// <summary>
        /// 目前只支持圆形碰撞体作为可移动物体
        /// 暂时未处理两个圆形可移动物体
        /// </summary>
        /// <param name="entity1"></param>
        /// <param name="entity2"></param>
        /// <param name="offset">可移动物体需要矫正的向量</param>
        /// <returns>是否产生了碰撞</returns>
        public static bool TryToCollision(PhysicEntity entity1, PhysicEntity entity2, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            if (!entity1.IsUnit && !entity2.IsUnit) return false;
            if (entity1.Collider.IsTrigger || entity2.Collider.IsTrigger) return false;
            switch (entity1.Collider)
            {
                case CircleCollider _ when entity2.Collider is CircleCollider:
                    return ColliderCapAndCap(entity1, entity2, out offset);
                case CircleCollider _ when entity2.Collider is BoxCollider:
                    return ColliderCapAndBox(entity2, entity1, out offset);
                case BoxCollider _ when entity2.Collider is CircleCollider:
                    return ColliderCapAndBox(entity1, entity2, out offset);
                case BoxCollider _ when entity2.Collider is BoxCollider:
                {
                    FLog.FLog.Error("暂时不支持Box和Box碰撞检测");
                    return false;
                }
            }

            return false;
        }

        private static bool ColliderCapAndCap(PhysicEntity entity1, PhysicEntity entity2, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            var cir1 = (CircleCollider)entity1.Collider;
            var cir2 = (CircleCollider)entity2.Collider;
            var normal = entity1.Position - entity2.Position;
            var sqrDis = normal.sqrMagnitude;
            var sqrRadius = FPMath.Pow(cir1.Radius + cir2.Radius, 2);
            if (sqrDis >= sqrRadius) return false;
            normal.Normalize();
            offset = normal * (FPMath.Sqrt(sqrRadius) - FPMath.Sqrt(sqrDis));
            return true;
        }

        private static bool ColliderCapAndBox(PhysicEntity boxEntity, PhysicEntity circleEntity, out FPVector2 offset)
        {
            offset = FPVector2.zero;
            var box = (BoxCollider)boxEntity.Collider;
            var circle = (CircleCollider)circleEntity.Collider;
            var disOffset = circleEntity.Position - boxEntity.Position;
            var xOffset = FPVector2.Dot(disOffset, boxEntity.Right);
            var yOffset = FPVector2.Dot(disOffset, boxEntity.Forward);
            var halfSizeX = box.Size.x / 2;
            var halfSizeY = box.Size.y / 2;
            var xMax = halfSizeX + circle.Radius;
            var yMax = halfSizeY + circle.Radius;
            if (FPMath.Abs(xOffset) >= xMax || FPMath.Abs(yOffset) >= yMax) return false;
            xOffset = FPMath.Clamp(xOffset, -halfSizeX, halfSizeX);
            yOffset = FPMath.Clamp(yOffset, -halfSizeY, halfSizeY);
            var point = boxEntity.Forward * yOffset + boxEntity.Right * xOffset + boxEntity.Position;
            //需要向外移动的方向
            var capOutDir = circleEntity.Position - point;
            var len = capOutDir.magnitude;
            //如果point正好在圆心，则需要特殊处理来获得方向
            if (capOutDir == FPVector2.zero)
            {
                capOutDir = circleEntity.Position - point / 2;
            }
            capOutDir.Normalize();
            offset = capOutDir * (circle.Radius - len);
            return true;
        }
    }
}