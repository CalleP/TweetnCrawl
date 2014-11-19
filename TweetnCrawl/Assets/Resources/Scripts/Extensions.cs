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
}

