using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Test : MonoBehaviour
{

    public SphereCollider Sphere;
    public BoxCollider Box;
    
    void Start()
    {
        GetComponents<>()
    }

    // Update is called once per frame
    void Update()
    {
        print(Intersects(Sphere, Box));
    }

    bool Intersects(SphereCollider sphereCollider, BoxCollider boxCollider)
    {
        
        Matrix4x4 rota = Matrix4x4.Rotate(Quaternion.Euler(-boxCollider.transform.eulerAngles));
        Vector3 distance =  (Vector3)(rota *sphereCollider.transform.position) - boxCollider.transform.position;
        distance = new Vector3(Mathf.Abs(distance.x), 0, Mathf.Abs(distance.z));
        var halfWidth = boxCollider.size.x / 2 * boxCollider.transform.lossyScale.x;
        var halfHeight = boxCollider.size.z / 2 * boxCollider.transform.lossyScale.z;
        var radius = sphereCollider.radius * sphereCollider.transform.localScale.x;

        if (distance.x > halfWidth + radius) return false;
        if (distance.z > halfHeight + radius) return false;

        if (distance.x <= halfWidth) return true;
        if (distance.z <= halfHeight) return true;

        var sqrDis = Mathf.Pow(distance.x - halfWidth, 2) +
                     Mathf.Pow(distance.z - halfHeight, 2);
        return sqrDis <= Mathf.Pow(radius, 2);
    }
}
