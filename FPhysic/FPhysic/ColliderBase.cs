using System;
using FMath;
using UnityEngine;

namespace FPhysic
{
    public class ColliderBase
    {
        public FPVector2 Center { get; }
        public bool IsTrigger { get; }
        protected PhysicEntity PhysicEntity { get; private set; }

        public ColliderBase( FPVector2 center, bool isTrigger = false)
        {
            Center = center;
            IsTrigger = isTrigger;
        }

        internal void SetEntity(PhysicEntity entity)
        {
            PhysicEntity = entity;
        }
    }
}