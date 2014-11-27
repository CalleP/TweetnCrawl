using UnityEngine;
using System.Collections;

public class ScalingScript : MonoBehaviour
{

    float virtualWidth = 1920.0f;
    float virtualHeight = 1080.0f;

    Matrix4x4 matrix;
    void Start()
    {

    }

    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }
}


