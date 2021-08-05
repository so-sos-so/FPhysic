using FMath;
using FPhysic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Start()
    {
        var collider = GetComponent<Collider>();
        var physicEntity = PhysicEntity.Create(false, collider);
        var forward = transform.forward;
        var right = transform.right;
        var scale = transform.localScale;
        physicEntity.Forward = new FPVector2(forward.x, forward.z);
        physicEntity.Right = new FPVector2(right.x, right.z);
        physicEntity.Scale = new FPVector2(scale.x, scale.z);
        ColliderCtrl.Ins.AddEntity(physicEntity, transform);
        physicEntity.Position = new FPVector2(transform.position.x, transform.position.z);
        physicEntity.Rotation = transform.eulerAngles.y;
        Object.Destroy(collider);
    }
}
