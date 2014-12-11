using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {



    public EnemyTypes EnemyType = EnemyTypes.Basic;
    public TerrainType terrainType = TerrainType.BlackCaste;

	public float speed; //The speed of the enemy
	public float chaseRange; //The chaserange of the enemy
	public Transform player; //The reference to the player	
	public Transform Follower; //The reference to the enemy
	public float distance; //The distance between the player and the enemy
	public int health = 100; //The enemy's health
	public float PrevSpeed; //The enemies speed stored when it needs to be restored to it's default value
	public CharacterHealth ch; //Access to the players health
	public double attackTime = 0.0; //Enemy attacktime
	public double AttackDelay = 1.0; //Enemy attack cooldown
	public float rotationSpeed = 50f; //The speed of which an enemy rotates when patrolling
	public float rotationTime = 5f; //The time it takes for the enemy to rotate
	public AudioClip jab; //The enemy's attack sound
	public float patrolspeed = 5; //The speed of which an enemy patrol at
	public bool waiting = false; //The boolean if the enemy has moved and is standing still true/false
	public Vector3 randomPosition; //The position an enemy will randomly move to when patrolling
	public Quaternion qTo; //Rotation reference
	public GameObject Projectile; //Enemy Projectiles
	protected bool state = true; //which state is the enemy in
	protected SpriteRenderer sr; //Renderer
	public float idleInterval = 0.5f; //Sprite interval
	protected float time; //Time
	bool spotted = false;
	protected float _posX;
	public AudioClip[] PlayerSpottedSounds;
	public AudioClip[] MonsterHitSounds;

    



	
	//Method containing the basereferences for the enemies
	public void baseReferences() {
		player = GameObject.Find("Player").transform; //Reference and find the playerobject
		attackTime = Time.time; //Attacktime is set to actual time
		CharacterHealth ch = GameObject.Find ("Player").GetComponent<CharacterHealth> (); //Reference to the players health
		chaseRange = 30.0f; //Sets the chaserange
		speed = Random.Range(10,20); //Randomizes the enemy's speed of a value between 10-20
		Follower = transform; //References the Enemy
		PrevSpeed = speed; //Stores the speed if we need to resume that speed later.
		rigidbody2D.fixedAngle = true;
		_posX = transform.position.x;

		PlayerSpottedSounds =  new AudioClip[]{(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterSpotPlayer/1"),
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterSpotPlayer/2"), 
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterSpotPlayer/3"), 
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterSpotPlayer/4")};


		
		MonsterHitSounds =  new AudioClip[]{(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterHits/m1"),
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterHits/m2"), 
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterHits/m3"), 
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterHits/m4"), 
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterHits/m5"), 
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterHits/m6"),
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterHits/m7"), 
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterHits/m8"), 
			(AudioClip)Resources.Load("Sounds/MonsterSounds/MonsterHits/m9")};

		}


	//Method dealing damage to the enemies
	public void receiveDamage (int dmg) {
		health = health - dmg;
		Debug.Log("Recieved this amount of damage "+dmg.ToString()+" now health="+health.ToString() );
        InterruptPatrol();
		StartCoroutine (Monsterhit());
        StartCoroutine(OnHitEffect());
	}

    public IEnumerator OnHitEffect()
    {

        yield return new WaitForSeconds(0.003f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.003f);
        renderer.material.color = Color.red;
        renderer.enabled = true;
        yield return new WaitForSeconds(0.003f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.003f);
        renderer.material.color = Color.black;
        renderer.enabled = true;
        yield return new WaitForSeconds(0.003f);
        renderer.material.color = Color.white;

        yield return null;
    }

    private bool dead = false;
    public IEnumerator OnDeathEffect()
    {
        if (!dead)
        {
            dead = true;
            var shadow = gameObject.transform.FindChild("Shadow");

            yield return new WaitForSeconds(0.006f);
            renderer.enabled = false;
            shadow.renderer.enabled = false;
            yield return new WaitForSeconds(0.006f);
            shadow.renderer.enabled = true;
            renderer.enabled = true;
            yield return new WaitForSeconds(0.006f);
            shadow.renderer.enabled = false;
            renderer.enabled = false;
            yield return new WaitForSeconds(0.006f);
            shadow.renderer.enabled = true;
            renderer.enabled = true;
            yield return new WaitForSeconds(0.006f);
            shadow.renderer.enabled = false;
            renderer.enabled = false;
            Instantiate(Resources.Load<GameObject>("Explosion"), transform.position, Quaternion.identity);
            Destroy(gameObject);

            yield return null;
        }

    }

	//Shootattack for the enemies
	public void shootAttack() {
		
		//Instantiate(Projectile, transform.position + (player.position - transform.position).normalized, transform.rotation);    
			
	}
	//MeleeAttack for the enemies
	public void meleeAttack() {
		int EnemyDamage = Random.Range (10,20);
		GameObject.Find("Player").GetComponent<CharacterHealth>().receiveDamage(EnemyDamage);
		this.audio.Play ();
	}
	
	//Patrol method for the enemies
	public void patrol() {
		
		if (waiting == false)
		{

                StopCoroutine(patrolUpdate());
                StartCoroutine(patrolUpdate());


		}
		else
		{
            if (timer <= Time.time)
            {
                var newPos = Vector2.MoveTowards(transform.position, randomPosition, speed * Time.deltaTime);
                rigidbody2D.MovePosition(newPos);
            }

			//transform.position = Vector3.Lerp (transform.position, randomPosition, Time.deltaTime);
			//transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime);
			
		}
	}
	
	
	protected virtual IEnumerator patrolUpdate() {
		int randomWait = Random.Range (1, 3);
		int	rotation = Random.Range (1,2);
		randomPosition = new Vector3 (transform.position.x + Random.Range( 10f,-10f ), transform.position.y + Random.Range( 8f, -8f ), 0f);
		if(rotation == 1) {
			qTo = Quaternion.Euler(new Vector3(0.0f,0.0f,Random.Range(-90.0f, 180.0f)));  
		} else if (rotation == 2) {
			qTo = Quaternion.Euler(new Vector3(0.0f,0.0f,Random.Range(-90.0f, 180.0f)));  
		}
		waiting = true;
		yield return new WaitForSeconds(randomWait);

		waiting = false;
		
	}

    private float timer;
    private float interruptTime = 0.75f;

    public void InterruptPatrol()
    {
        timer = Time.time + interruptTime;
    }

	//TODO this method needs more tweaking
	protected virtual IEnumerator RandomPlayerSpottedSound() {
		if (distance < chaseRange - 25 && !audio.isPlaying) {
			int wait = Random.Range(3,20);
			yield return new WaitForSeconds(wait);
			audio.PlayOneShot (PlayerSpottedSounds [Random.Range (0, PlayerSpottedSounds.Length)]);
				}
		yield return null;
		
	}


	protected virtual IEnumerator Monsterhit() {

		AudioSource.PlayClipAtPoint(MonsterHitSounds [Random.Range (0, MonsterHitSounds.Length)], transform.position);

		yield return null;
		
	}

	//Chase method for the enemies
	public void chase() {
		//float z = Mathf.Atan2 ((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
		//transform.eulerAngles = new Vector3 (0, 0, z);
		//rigidbody2D.AddForce(transform.up * speed);
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
	}

	public void Flip() {
        if (transform.position.x == _posX)
        {
            
        }
		else if (transform.position.x < _posX)
		{
		
				transform.eulerAngles = new Vector3(0, 0, 0);
		
		}
		else
		{
		
				transform.eulerAngles = new Vector3(0,180,0);
		
		}
		
		_posX = transform.position.x;
		}


    protected bool isPlayerInLineOfSight()
    {
        distance = Vector3.Distance(Follower.position, player.position);
        bool hitPlayer = false;
        bool LOS = true;
		StartCoroutine (RandomPlayerSpottedSound());
		

        var hits = Physics2D.LinecastAll((Vector2)transform.position, (Vector2)player.transform.position);
        for (int i = 0; i < hits.Length; i++)
        {
            var hitDistance = (hits[i].point - (Vector2)transform.position).magnitude;
            if (hits[i].transform.gameObject.tag == "Player")
            {
                hitPlayer = true;
            }
            else if (hits[i].transform.gameObject.tag == "Wall" && hitDistance <= distance)
            {
                LOS = false;
            }
        }
        if (hitPlayer && LOS)
        {
            return true;

        }
        return false;

    }
}
