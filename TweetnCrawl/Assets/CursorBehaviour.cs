using UnityEngine;
using System.Collections;

public class CursorBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () 
    {

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
	}
}
