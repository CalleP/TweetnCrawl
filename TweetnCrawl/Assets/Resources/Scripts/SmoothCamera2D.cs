using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//Script found at http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html
using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{


    // Update is called once per frame
    public float Damping = 5.0f;
    public Transform Player;
    public float Height = 4f;
    public float Offset = 5f;
 
    private Vector3 Center;
    float ViewDistance  = 5.0f;

    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = ViewDistance;
        Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(mousePos);

        var PlayerPosition = Player.position;

        Center = new Vector3((PlayerPosition.x + CursorPosition.x) / 2, (PlayerPosition.y + CursorPosition.y) / 2, (PlayerPosition.z + CursorPosition.z) / 2);

        transform.position = Vector3.Lerp(transform.position, Center + new Vector3(0, Height, Offset), Time.deltaTime * Damping);
        transform.position = new Vector3(transform.position.x, transform.position.y, -11);

    }

}

