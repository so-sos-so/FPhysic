using FMath;
using FPhysic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    void Start()
    {
        PhysicEntity physicEntity = new PhysicEntity();
        var forward = transform.forward;
        var right = transform.right;
        var scale = transform.localScale;
        physicEntity.Forward = new FPVector2(forward.x, forward.z);
        physicEntity.Right = new FPVector2(right.x, right.z);
        physicEntity.Scale = new FPVector2(scale.x, scale.z);
        physicEntity.AddComponent(ColliderBase.Create(physicEntity, gameObject));
        ColliderCtrl.Ins.AddEntity(physicEntity, transform);
        physicEntity.Position = new FPVector2(transform.position.x, transform.position.z);
        physicEntity.Rotation = transform.eulerAngles.y;
    }
}
