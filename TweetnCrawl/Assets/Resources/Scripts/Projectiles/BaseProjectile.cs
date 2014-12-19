using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour {

    Vector3 startPosition;
    public Vector2 direction;
    public Quaternion rotation;
    public float speed;
    public BaseWeapon weapon = null;

    public float ForceMultiplier = 12f;
    public GameObject onDeathPrefab;

    public int Damage = 50;
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

    }

    public void Init(Vector2 direction, Quaternion rotation, float speed, int damage, BaseWeapon weapon)
    {
        Init(direction,rotation,speed,damage);
        this.weapon = weapon;

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
            coll.rigidbody2D.AddForce(rigidbody2D.velocity*ForceMultiplier);


            StartCoroutine(OnDeath(true));
            
        }

		if (coll.gameObject.tag == "Enemy2")
		{
			coll.GetComponent<EnemyScript2>().receiveDamage(Damage);
            StartCoroutine(OnDeath(true));
		}
		if (coll.gameObject.tag == "Splitter")
		{
			coll.GetComponent<Splitter>().receiveDamage(Damage);
            StartCoroutine(OnDeath(true));
		}


        if (coll.gameObject.tag == "Wall")
        {
            StartCoroutine(OnDeath(false));
        }
        if (coll.gameObject.tag == "Sign")
        {
            StartCoroutine(OnDeath(true));
            Destroy(coll.gameObject);
        }

    }


    protected virtual IEnumerator OnDeath(bool hitEnemy)
    {

        spawnDeathAnim(hitEnemy);



        renderer.enabled = false;
        collider2D.enabled = false;
        while (audio.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);


    }


    private static System.Random rand = new System.Random(); 
    protected void spawnDeathAnim(bool hitEnemy)
    {
        if (onDeathPrefab != null)
        {

            var obj = (GameObject)Instantiate(onDeathPrefab, new Vector3(transform.position.x,transform.position.y, -0), Quaternion.identity);
            obj.transform.Rotate(new Vector3(0, 0, rand.Next(0,360)));

            if (weapon != null && hitEnemy)
            {
                obj.GetComponent<OnHitEffect>().PauseTime = weapon.PauseOnHit;
                obj.GetComponent<OnHitEffect>().HitEnemy = hitEnemy;
            }
                


            
        }

    }



}
