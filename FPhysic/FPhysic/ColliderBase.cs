using System;
using FMath;
using UnityEngine;

namespace FPhysic
{
    public class ColliderBase
    {
        public FPVector3 Center { get; }
        protected FPVector3 Position => Entity.Position + Center;
        protected FPVector3 Rotation => Entity.Rotation;
        protected FPInt Mess;
        protected FPVector3 Scale => Entity.Scale;
        protected Entity Entity;

        public ColliderBase(Entity entity, FPVector3 center)
        {
            Center = center;
            Entity = entity;
        }

        public static bool Intersects(ColliderBase collider1, ColliderBase collider2)
        {
            return collider1 switch
            {
                CapsuleCollider capsuleCollider when collider2 is CapsuleCollider capsuleCollider2 => Intersects(
                    capsuleCollider, capsuleCollider2),
                CapsuleCollider capsuleCollider when collider2 is BoxCollider boxCollider => Intersects(boxCollider,
                    capsuleCollider),
                BoxCollider boxCollider when collider2 is CapsuleCollider capsuleCollider2 => Intersects(boxCollider,
                    capsuleCollider2),
                BoxCollider boxCollider when collider2 is BoxCollider boxCollider2 => Intersects(boxCollider,
                    boxCollider2),
                _ => throw new Exception("????")
            };
        }

        /// <summary>
        /// 方形和圆形是否交叉
        /// </summary>
        public static bool Intersects(BoxCollider boxCollider, CapsuleCollider capsuleCollider)
        {
            FPMatrix4x4 rota = FPMatrix4x4.Rotate(-boxCollider.Rotation);
            FPVector3 distance = (FPVector3) (rota * capsuleCollider.Position) - boxCollider.Position;
            distance = new FPVector3(FPMath.Abs(distance.x), 0, FPMath.Abs(distance.z));
            var halfWidth = boxCollider.Size.x / 2 * boxCollider.Scale.x;
            var halfHeight = boxCollider.Size.z / 2 * boxCollider.Scale.z;
            var radius = capsuleCollider.Radius * capsuleCollider.Scale.x;

            if (distance.x > halfWidth + radius) return false;
            if (distance.z > halfHeight + radius) return false;

            if (distance.x <= halfWidth) return true;
            if (distance.z <= halfHeight) return true;

            var sqrDis = FPMath.Pow(distance.x - halfWidth, 2) +
                         FPMath.Pow(distance.z - halfHeight, 2);
            return sqrDis <= FPMath.Pow(radius, 2);
        }

        public static bool Intersects(BoxCollider boxCollider1, BoxCollider boxCollider2)
        {
            return true;
        }

        /// <summary>
        /// 两个圆形是否交叉
        /// </summary>
        public static bool Intersects(CapsuleCollider capsuleCollider1, CapsuleCollider capsuleCollider2)
        {
            var sqrDis = FPMath.Pow(capsuleCollider1.Radius + capsuleCollider2.Radius, 2);
            return (capsuleCollider1.Position - capsuleCollider2.Position).sqrMagnitude < sqrDis;
        }

        #region 各个工厂

        /// <summary>
        /// 传进来的collider需要有一个标准
        /// 物体的scale为1
        /// 碰撞修改collider上的属性
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public static ColliderBase Create(Entity entity, Collider collider)
        {
            ColliderBase result = collider switch
            {
                UnityEngine.BoxCollider boxCollider => new BoxCollider(entity, boxCollider.center, boxCollider.size),
                UnityEngine.CapsuleCollider capsuleCollider => new CapsuleCollider(entity, capsuleCollider.center,
                    capsuleCollider.radius),
                _ => throw new NullReferenceException($"在{collider.name}上没有找到BoxCollider或者CapsuleCollider")
            };

            return result;
        }

        /// <summary>
        /// 传进来的collider需要有一个标准
        /// 物体的scale为1
        /// 碰撞修改collider上的属性
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public static ColliderBase Create(Entity entity, GameObject gameObject)
        {
            return Create(entity,gameObject.GetComponent<Collider>());
        }

        public static ColliderBase Create(Entity entity,FPVector3 center, FPVector3 size)
        {
            return new BoxCollider(entity, center, size);
        }
        
        public static ColliderBase Create(Entity entity,FPVector3 center,FPInt radius)
        {
            return new CapsuleCollider(entity,center, radius);
        }
        #endregion
    }
}