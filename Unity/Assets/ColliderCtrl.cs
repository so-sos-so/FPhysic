using System;
using System.Collections.Generic;
using FPhysic;
using UnityEngine;

public class ColliderCtrl : MonoBehaviour
{
    public static ColliderCtrl Ins;
    private List<PhysicEntity> colliders = new List<PhysicEntity>();
    private Dictionary<PhysicEntity, Transform> players = new Dictionary<PhysicEntity, Transform>();
    
    private void Awake()
    {
        Ins = this;
    }

    public void AddEntity(PhysicEntity entity, Transform go)
    {
        if (entity.IsUnit)
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