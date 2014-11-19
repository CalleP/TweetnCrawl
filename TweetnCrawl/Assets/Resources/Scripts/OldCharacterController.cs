using UnityEngine;
using System.Collections;

public class OldCharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


    void Update()
    {
        transform.rotation = Quaternion.identity;
        rigidbody2D.velocity = new Vector2(0,0);
    }
	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation = Quaternion.identity;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0.32f, transform.position.z), 1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);

        }
        if (Input.GetKey(KeyCode.S))
        {
             transform.position = Vector3.MoveTowards(transform.position, transform.position+new Vector3(0, -0.32f, transform.position.z), 1f);
             transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-0.32f, 0, transform.position.z), 1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0.32f, 0, transform.position.z), 1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
	}
}
