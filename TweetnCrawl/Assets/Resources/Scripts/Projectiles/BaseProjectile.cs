using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour {

    Vector3 startPosition;
    public Vector2 direction;
    public Quaternion rotation;
    public float speed;



    public int Damage = 50;
	// Use this for initialization
    public int SpriteRotation = -90;

    public void Init(Vector2 direction, Quaternion rotation, float speed, int damage)
    {
        Damage = damage;
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
	public virtual void FixedUpdate () {
        rigidbody2D.velocity = transform.up*speed;
        
        

	}

    public virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.GetComponent<EnemyScript>().receiveDamage(Damage);
            Destroy(gameObject);
        }

		if (coll.gameObject.tag == "Player")
		{
			coll.GetComponent<CharacterHealth>().receiveDamage(Damage);
			Destroy(gameObject);
		}

        if (coll.gameObject.tag == "Wall")
        {
            
            Destroy(gameObject);
        }
    }



}
