using System;
using FMath;
using FPhysic;
using UnityEngine;
using Rigidbody = FPhysic.Rigidbody;

public class Player : MonoBehaviour
{
    private PhysicEntity physicEntity;
    public float Speed;
    private void Start()
    {
        physicEntity = new PhysicEntity();
        var forward = transform.forward;
        var right = transform.right;
        var scale = transform.localScale;
        physicEntity.Forward = new FPVector2(forward.x, forward.z);
        physicEntity.Right = new FPVector2(right.x, right.z);
        physicEntity.Scale = new FPVector2(scale.x, scale.z);
        physicEntity.AddComponent(ColliderBase.Create(physicEntity, gameObject));
        physicEntity.AddComponent(new Rigidbody());
        ColliderCtrl.Ins.AddEntity(physicEntity, transform);
        physicEntity.Rotation = transform.eulerAngles.y;
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