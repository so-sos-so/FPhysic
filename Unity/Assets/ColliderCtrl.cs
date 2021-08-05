using System;
using System.Collections.Generic;
using FPhysic;
using UnityEngine;
using Rigidbody = FPhysic.Rigidbody;

public class ColliderCtrl : MonoBehaviour
{
    public static ColliderCtrl Ins;
    private List<Entity> colliders = new List<Entity>();
    private Dictionary<Entity, Transform> players = new Dictionary<Entity, Transform>();
    
    private void Awake()
    {
        Ins = this;
    }

    public void AddEntity(Entity entity, Transform go)
    {
        if (entity.GetComponent<Rigidbody>() != null)
        {
            players[entity] = go;
        }
        else
        {
            colliders.Add(entity);
        }
    }

    private void LateUpdate()
    {
        foreach (var player in players)
        {
            foreach (var collider in colliders)
            {
                if (FPhysic.ColliderCtrl.TryToCollision(player.Key, collider, out var offset))
                {
                    player.Value.position += new Vector3(offset.x.RawFloat, 0, offset.y.RawFloat);
                }
            }
        }
    }
}