using System;
using FMath;
using UnityEngine;

namespace FPhysic
{
    public class ColliderBase
    {
        public FPVector3 Center { get; }
        protected FPVector3 Position => Entity.Position + Center;
        protected FPVector3 Forward => Entity.Forward;
        protected FPVector3 Right => Entity.Right;
        protected IEntity Entity;

        public ColliderBase(FPVector3 center)
        {
            Center = center;
        }

        public void SetEntity(IEntity entity)
        {
            Entity = entity;
        }
        
        public virtual bool TryInteraction(ColliderBase other,out FPVector3 result)
        {
            result = FPVector3.zero;
            if (this is BoxCollider boxCollider && other is CapsuleCollider capsuleCollider)
            {
                return CapsuleCrossBox(boxCollider, capsuleCollider, out result);
            }
            return true;
        }

        protected bool CapsuleCrossBox(BoxCollider boxCollider, CapsuleCollider capsuleCollider, out FPVector3 result)
        {
            result = FPVector3.zero;
            var posOffset = capsuleCollider.Position - boxCollider.Position;
            var zLength = FPVector3.Dot(boxCollider.Forward, posOffset);
            var xLength = FPVector3.Dot(boxCollider.Right, posOffset);
            var boxSize = boxCollider.Size;
            if (zLength > boxSize.z || xLength > boxSize.z) return false;
            if (zLength < boxSize.z + capsuleCollider.Radius && xLength < boxSize.x + capsuleCollider.Radius)
            {
                result.z = (zLength - boxSize.z - capsuleCollider.Radius) / 2;
            }
            return true;
        }
        
        protected bool CapsuleCrossCapsule(CapsuleCollider capsuleCollider1, CapsuleCollider capsuleCollider2, out FPVector3 result)
        {
            result = FPVector3.zero;
            if ((capsuleCollider1.Position - capsuleCollider2.Position).sqrMagnitude >
                FPMath.Pow(capsuleCollider1.Radius + capsuleCollider2.Radius, 2)) return false;
            
            return true;
        }
        

        #region 各个工厂
        /// <summary>
        /// 传进来的collider需要有一个标准
        /// 物体的scale为1
        /// 碰撞修改collider上的属性
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static ColliderBase Create(Collider collider)
        {
            ColliderBase result = null;
            if (collider is UnityEngine.BoxCollider boxCollider)
            {
                result = new BoxCollider(boxCollider.center, boxCollider.size);
            }else if (collider is UnityEngine.CapsuleCollider capsuleCollider)
            {
                result = new CapsuleCollider(capsuleCollider.center, capsuleCollider.radius);
            }
            else
            {
                throw new NullReferenceException($"在{collider.name}上没有找到BoxCollider或者CapsuleCollider");
            }
            return result;
        }

        /// <summary>
        /// 传进来的collider需要有一个标准
        /// 物体的scale为1
        /// 碰撞修改collider上的属性
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static ColliderBase Create(GameObject gameObject)
        {
            return Create(gameObject.GetComponent<Collider>());
        }
        
        public static ColliderBase Create(FPVector3 center, FPVector3 size)
        {
            return new BoxCollider(center, size);
        }
        
        public static ColliderBase Create(FPVector3 center,FPInt radius)
        {
            return new CapsuleCollider(center, radius);
        }
        #endregion
    }
}