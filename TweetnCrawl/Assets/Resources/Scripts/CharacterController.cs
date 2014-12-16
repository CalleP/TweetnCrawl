using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{

    float speed = 0f;
    void Update()
    {
        var maxSpeed = 0.064f;
        var k = transform.up;
        var k2 = transform.right;
        var accel = 0.01f;
        var moveAmount = 0;
        if (Input.GetKey(KeyCode.W))
        {
            speed += accel;
            transform.Translate(k * speed);
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed += accel;
            transform.Translate((k * -1) * speed);
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            speed += accel;
            transform.Translate((k2 * -1) * speed);
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            speed += accel;
            transform.Translate(k2 * speed);
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }

        int Keysdown = 0;
        bool areTwoKeysDown = false;

        if (Input.GetKey(KeyCode.W))
            Keysdown++;
        if (Input.GetKey(KeyCode.S))
            Keysdown++;
        if (Input.GetKey(KeyCode.A))
            Keysdown++;
        if (Input.GetKey(KeyCode.D))
            Keysdown++;
        if (Keysdown > 1)
        {
            areTwoKeysDown = true;
        }

        if (areTwoKeysDown == true)
        {
            maxSpeed = maxSpeed / 2;
        }
    }
}