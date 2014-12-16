using UnityEngine;
using System.Collections;

public class MouseFollower : MonoBehaviour {

	void Update () 
    {
	    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
