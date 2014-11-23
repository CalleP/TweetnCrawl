using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour {

    Vector3 startPosition;
    public Vector2 direction;
    public Quaternion rotation;
    public float speed;

    public GameObject onDeathPrefab;

    public int Damage = 50;
	// Use this for initialization
    public int SpriteRotation = -90;

    void Start()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
    }

    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
    
    }


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
            coll.GetComponent<BaseEnemy>().receiveDamage(Damage);
            spawnDeathAnim();
            Destroy(gameObject);
            
        }

		if (coll.gameObject.tag == "Enemy2")
		{
			coll.GetComponent<EnemyScript2>().receiveDamage(Damage);
			spawnDeathAnim();
			Destroy(gameObject);
		}


        if (coll.gameObject.tag == "Wall")
        {
            spawnDeathAnim();
            Destroy(gameObject);
        }
    }

    private static System.Random rand = new System.Random(); 
    protected void spawnDeathAnim()
    {
        if (onDeathPrefab != null)
        {

            var obj = (GameObject)Instantiate(onDeathPrefab, new Vector3(transform.position.x,transform.position.y, -5), Quaternion.identity);
            obj.transform.Rotate(new Vector3(0, 0, rand.Next(0,360)));
        }

    }



}
