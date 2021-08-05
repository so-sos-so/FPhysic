using System;
using System.Collections.Generic;
using FMath;
using UnityEngine;

namespace FPhysic
{
    public class PhysicEntity
    {
        public FPVector2 Position { get; set; } = FPVector2.zero;
        public FPInt Rotation { get; set; } = 0;
        public FPVector2 Scale { get; set; } = FPVector2.one;
        public FPVector2 Forward { get; set; } = new FPVector2(0, 1);
        public FPVector2 Right { get; set; } = new FPVector2(1, 0);
        public ColliderBase Collider { get; }
        public bool IsUnit { get; }

        private PhysicEntity(bool isUnit, ColliderBase collider)
        {
            IsUnit = isUnit;
            Collider = collider;
        }
        
        internal void TransformIdentity()
        {
            Position = FPVector2.zero;
            Rotation = 0;
            Forward = new FPVector2(0, 1);
            Right = new FPVector2(1, 0);
            Scale = FPVector2.one;
        }
        
        public static PhysicEntity Create(bool isUnit, Collider collider)
        {
            ColliderBase innerCollider = null; 
            switch (collider)
            {
                case UnityEngine.BoxCollider boxCollider:
                    innerCollider = new BoxCollider(new FPVector2(boxCollider.center.x, boxCollider.center.z),
                        new FPVector2(boxCollider.size.x, boxCollider.size.z));
                    break;
                case UnityEngine.CapsuleCollider capsuleCollider:
                    innerCollider = new CircleCollider(new FPVector2(capsuleCollider.center.x, capsuleCollider.center.z),
                        capsuleCollider.radius);
                    break;
                case UnityEngine.SphereCollider sphereCollider:
                    innerCollider = new CircleCollider(new FPVector2(sphereCollider.center.x, sphereCollider.center.z),
                        sphereCollider.radius);
                    break;
            }

            if (innerCollider == null)
                throw new NullReferenceException($"在{collider.name}上没有找到BoxCollider或者CapsuleCollider");
            PhysicEntity entity = new PhysicEntity(isUnit, innerCollider);
            innerCollider.SetEntity(entity);
            return entity;
        }

        public static PhysicEntity Create(bool isUnit, FPVector2 center, FPVector2 size)
        {
            var collider = new BoxCollider(center, size);
            PhysicEntity entity = new PhysicEntity(isUnit, collider);
            collider.SetEntity(entity);
            return entity;
        }

        public static PhysicEntity Create(bool isUnit, FPVector2 center, FPInt radius)
        {
            var collider = new CircleCollider(center, radius);
            PhysicEntity entity = new PhysicEntity(isUnit, collider);
            collider.SetEntity(entity);
            return entity;
        }
    }
}