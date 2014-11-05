using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour {

    Vector3 startPosition;
    public Vector2 direction;
    public Quaternion rotation;
    public float speed;

    public int Damage = 1;
	// Use this for initialization

    public void Init(Vector2 direction, Quaternion rotation, float speed)
    {

        this.direction = direction;
        this.rotation = rotation;
        
        this.speed = speed;
        transform.rotation = rotation;
        var rand = new System.Random();
        transform.Rotate(new Vector3(0, 0, -90));

        this.direction = direction;
        //rigidbody2D.velocity = direction * speed;
        //rigidbody2D.velocity = direction;

    }


	
   

	// Update is called once per frame
	void FixedUpdate () {
        rigidbody2D.velocity = transform.up*speed;
        
        

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Enemy")
        {
            coll.GetComponent<BaseEnemy>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }



}
