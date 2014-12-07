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

    void Start()
    { 
        //camera.orthographicSize = Screen.width / 64 / 2;

    }

    void Update()
    {


        var mousePos = Input.mousePosition;
        mousePos.z = ViewDistance;
        Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(mousePos);

        var PlayerPosition = Player.position;

        Center = new Vector3((PlayerPosition.x + CursorPosition.x) / 2, (PlayerPosition.y + CursorPosition.y) / 2, (PlayerPosition.z + CursorPosition.z) / 2);

        transform.position = Vector3.Lerp(transform.position, Center + new Vector3(0, Height, Offset), Time.deltaTime * Damping);
        transform.position = new Vector3(transform.position.x, transform.position.y, -11);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //transform.position = new Vector3(transform.position.x + (Mathf.PerlinNoise(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)) / 4), transform.position.y + (Mathf.PerlinNoise(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)) / 4), transform.position.z);
            
        }

    }

    public void shake(float magnitude, float duration) 
    {
        if (duration == null)
        {
            duration = 0.1f;
        }
        StartCoroutine(Shake(0.1f, duration));
    }

    IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = UnityEngine.Random.value * 2.0f - 1.0f;
            float y = UnityEngine.Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, originalCamPos.z);

            yield return null;
        }

        //Camera.main.transform.position = originalCamPos;
    }

}

