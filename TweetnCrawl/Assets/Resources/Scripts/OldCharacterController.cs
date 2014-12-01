using UnityEngine;
using System.Collections;

public class OldCharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
	}
    public Sprite IdleState1;
    public Sprite IdleState2;
    private bool state = true;
    private SpriteRenderer sr;

    public float idleInterval = 0.5f;
    private float time;

    void Update()
    {

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        rigidbody2D.isKinematic = true;
        rigidbody2D.isKinematic = false;
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

        if (Time.time >= time)
        {
            if (state == true)
            {
                state = false;
                sr.sprite = IdleState1;

            }
            else
            {
                state = true;
                sr.sprite = IdleState2;
            }
            time = Time.time + idleInterval;
        }






	}
}
