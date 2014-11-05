using UnityEngine;
using System.Collections;

public class OldCharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	

    
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0.32f));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -0.32f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-0.32f, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0.32f, 0));
        }
	}
}
