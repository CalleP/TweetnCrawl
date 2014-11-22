using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public float speed;
	public float chaseRange;
	public Transform player;
	public Transform Follower;
	public float distance;
	public int health = 100;
	public float PrevSpeed;
	public CharacterHealth ch;
	double attackTime = 0.0;
	double AttackDelay = 1.0;
	float rotationSpeed = 50f;
	public float rotationTime = 5f;
	public AudioClip jab;
	public float patrolspeed = 5;
	public bool waiting = false;
	Vector3 randomPosition;
	Quaternion qTo;


	
	void Start() 
	{
        player = GameObject.Find("Player").transform;
		attackTime = Time.time;
		CharacterHealth ch = GameObject.Find ("Player").GetComponent<CharacterHealth> ();
		chaseRange = 20.0f;
		speed = Random.Range(10,20);
		Follower = transform;
		PrevSpeed = speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if the enemy followers health reaches 0 remove him from the game.
		if (health <= 0) {
			print ("Blaaah you killed me!");
			Destroy ((Follower as Transform).gameObject);
		}
		
		//Updates constantly the distance between the follower and the player
		distance = Vector3.Distance (Follower.position, player.position);
		if (distance > chaseRange) {
						patrol ();
				}
		//if the distance gets within the chaseRange the follower will start following the player
		if (distance <= chaseRange) {

			
			float z = Mathf.Atan2 ((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
			
			transform.eulerAngles = new Vector3 (0, 0, z);
			
			rigidbody2D.AddForce(transform.up * speed);
			
			//if the enemy is close enough with a distance of 5 or less hit the player.
			if (distance <= chaseRange - 15f) {
				speed = 0;
				
				// a simple boolean checking if the enemy can attack or not to provide delay
				if (Time.time > attackTime && GameObject.Find("Player").GetComponent<CharacterHealth>().health >= 0) {
					
					EnemyAttack ();
					attackTime = Time.time + AttackDelay;
					
				} else {
					speed = PrevSpeed;
				}
				
			} else {
				return;
			}
		} 
	}
	
	//subtracts the enemy health with the player damage.
	public void receiveDamage (int dmg) {
		health = health - dmg;
		Debug.Log("Recieved this amount of damage "+dmg.ToString()+" now health="+health.ToString() );
	}
	
	public void EnemyAttack() {
		int EnemyDamage = Random.Range (10,20);
		GameObject.Find("Player").GetComponent<CharacterHealth>().receiveDamage(EnemyDamage);
		this.audio.Play ();
	}

	public void patrol() {

		if (waiting == false)
		{
			print("stopped patrol");
			StopCoroutine (patrolUpdate ());
			StartCoroutine (patrolUpdate ());
		}
		else
		{

			transform.position = Vector3.Lerp (transform.position, randomPosition, Time.deltaTime*1);
			transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * 10);
		
		}
	}
	
	
	IEnumerator patrolUpdate() {
	int	rotation = Random.Range (1,2);
		randomPosition = new Vector3 (transform.position.x + Random.Range( 10f,-10f ), transform.position.y + Random.Range( 8f, -8f ), 0f);
		if(rotation == 1) {
			qTo = Quaternion.Euler(new Vector3(0.0f,0.0f,Random.Range(-90.0f, 180.0f)));  
		} else if (rotation == 2) {
			qTo = Quaternion.Euler(new Vector3(0.0f,0.0f,Random.Range(-90.0f, 180.0f)));  
		}
		waiting = true;
		yield return new WaitForSeconds(2);
		waiting = false;

		}
	

	
	
}
