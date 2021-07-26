using System;

namespace FPhysic
{
    public class ColliderCtrl
    {
        public bool TryToCollision(Entity entity1, Entity entity2)
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

        private void Collider(CapsuleCollider collider1, Rigidbody rigidbody1, CapsuleCollider collider2,
            Rigidbody rigidbody2)
        {
            
        }
        
        private void Collider(BoxCollider collider1, Rigidbody rigidbody1, BoxCollider collider2,
            Rigidbody rigidbody2)
        {
            
        }
        
        private void Collider(BoxCollider collider1, Rigidbody rigidbody1, CapsuleCollider collider2,
            Rigidbody rigidbody2)
        {
            
        }
    }
}