using System;
using FMath;
using UnityEngine;

namespace FPhysic
{
    public class FColliderBase
    {
        public FPVector3 Center { get; }
        protected FPVector3 Position => Entity.Position + Center;
        protected FPVector3 Rotation => Entity.Rotation;
        protected IEntity Entity;

        public FColliderBase(FPVector3 center)
        {
            Center = center;
        }

        public void SetEntity(IEntity entity)
        {
            Entity = entity;
        }
        
        public virtual bool TryInteraction(FColliderBase other,out FPVector3 result)
        {
            result = FPVector3.zero;
            
            return true;
        }

        protected void CapsuleCrossBox(FBoxCollider boxCollider, FCapsuleCollider capsuleCollider)
        {
            
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
        public static FColliderBase Create(Collider collider)
        {
            FColliderBase result = null;
            if (collider is BoxCollider boxCollider)
            {
                result = new FBoxCollider(boxCollider.center, boxCollider.size);
            }else if (collider is CapsuleCollider capsuleCollider)
            {
                result = new FCapsuleCollider(capsuleCollider.center, capsuleCollider.radius);
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
        public static FColliderBase Create(GameObject gameObject)
        {
            return Create(gameObject.GetComponent<Collider>());
        }
        
        public static FColliderBase Create(FPVector3 center, FPVector3 size)
        {
            return new FBoxCollider(center, size);
        }
        
        public static FColliderBase Create(FPVector3 center,FPInt radius)
        {
            return new FCapsuleCollider(center, radius);
        }
        #endregion
    }
}