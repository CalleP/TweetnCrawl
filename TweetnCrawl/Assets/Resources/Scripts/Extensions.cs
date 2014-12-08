using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class ExtensionMethods
{

    public static void Relocate(this Transform trans, direction direction, int maxWidth, int maxHeight, int hubSize)
    {
        var pos = trans.position;
        switch (direction)
        {
            case direction.up:
                trans.position = new Vector3(pos.x, pos.y + ((maxHeight+hubSize)*3.2f), pos.z);
                break;
            case direction.down:
                trans.position = new Vector3(pos.x, pos.y - ((maxHeight + hubSize) * 3.2f), pos.z);
                break;
            case direction.left:
                trans.position = new Vector3(pos.x - ((maxWidth + hubSize) * 3.2f), pos.y, pos.z);
                break;
            case direction.right:
                trans.position = new Vector3(pos.x + ((maxWidth + hubSize) * 3.2f), pos.y, pos.z);
                break;
            default:
                break;
        }
    }

    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
    }
 
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * explosionForce * wearoff;
        body.AddForce(baseForce);
 
        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }
}


