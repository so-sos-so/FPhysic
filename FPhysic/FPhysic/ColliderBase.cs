using System;
using FMath;
using UnityEngine;

namespace FPhysic
{
    public class ColliderBase
    {
        public FPVector2 Center { get; }
        public FPVector2 Position => Entity.Position + Center;
        public FPInt Rotation => Entity.Rotation;
        public FPVector2 Scale => Entity.Scale;
        public FPVector2 Forward => Entity.Forward;
        public FPVector2 Right => Entity.Right;
        public bool IsTrigger { get; }
        protected Entity Entity;

        public ColliderBase(Entity entity, FPVector2 center, bool isTrigger = false)
        {
            Center = center;
            Entity = entity;
            IsTrigger = isTrigger;
        }

        /// <summary>
        /// 方形和圆形是否交叉
        /// </summary>
        public static bool Intersects(BoxCollider boxCollider, CapsuleCollider capsuleCollider)
        {
            FPMatrix4x4 rota = FPMatrix4x4.Rotate(new FPVector3(0, -boxCollider.Rotation, 0));
            FPVector3 distance = (FPVector3)(rota * new FPVector3(0, -capsuleCollider.Rotation, 0)) -
                                 new FPVector3(boxCollider.Position.x, 0, boxCollider.Position.y);
            distance = new FPVector3(FPMath.Abs(distance.x), 0, FPMath.Abs(distance.z));
            var halfWidth = boxCollider.Size.x / 2 * boxCollider.Scale.x;
            var halfHeight = boxCollider.Size.y / 2 * boxCollider.Scale.y;
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
            if (collider is UnityEngine.BoxCollider boxCollider)
            {
                return new BoxCollider(entity,
                    new FPVector2(boxCollider.center.x, boxCollider.center.z),
                    new FPVector2(boxCollider.size.x, boxCollider.size.z));
            }
            if (collider is UnityEngine.CapsuleCollider capsuleCollider)
            {
                return new CapsuleCollider(entity,
                    new FPVector2(capsuleCollider.center.x, capsuleCollider.center.z),
                    capsuleCollider.radius);
            }
            if (collider is UnityEngine.SphereCollider sphereCollider)
            {
                return new CapsuleCollider(entity,
                    new FPVector2(sphereCollider.center.x, sphereCollider.center.z),
                    sphereCollider.radius);
            }
            throw new NullReferenceException($"在{collider.name}上没有找到BoxCollider或者CapsuleCollider");
        }

        /// <summary>
        /// 传进来的collider需要有一个标准
        /// 物体的scale为1
        /// 碰撞修改collider上的属性
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public static ColliderBase Create(Entity entity, GameObject gameObject)
        {
            return Create(entity, gameObject.GetComponent<Collider>());
        }

        public static ColliderBase Create(Entity entity, FPVector3 center, FPVector3 size)
        {
            return new BoxCollider(entity, new FPVector2(center.x, center.z), new FPVector2(size.x, size.z));
        }

        public static ColliderBase Create(Entity entity, FPVector3 center, FPInt radius)
        {
            return new CapsuleCollider(entity, new FPVector2(center.x, center.z), radius);
        }

        #endregion
    }
}