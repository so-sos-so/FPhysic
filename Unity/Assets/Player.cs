using System;
using FMath;
using FPhysic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Player : MonoBehaviour
{
    private PhysicEntity physicEntity;
    public float Speed;
    private void Start()
    {
        var collider = GetComponent<Collider>();
        physicEntity = PhysicEntity.Create(true, collider);
        var forward = transform.forward;
        var right = transform.right;
        var scale = transform.localScale;
        physicEntity.Forward = new FPVector2(forward.x, forward.z);
        physicEntity.Right = new FPVector2(right.x, right.z);
        physicEntity.Scale = new FPVector2(scale.x, scale.z);
        ColliderCtrl.Ins.AddEntity(physicEntity, transform);
        physicEntity.Rotation = transform.eulerAngles.y;
        Object.Destroy(collider);
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        transform.position += new Vector3(h, 0, v) * (Time.deltaTime * Speed);
        var position = transform.position;
        physicEntity.Position = new FPVector2(position.x, position.z);
        physicEntity.Rotation = transform.eulerAngles.y;
    }
}