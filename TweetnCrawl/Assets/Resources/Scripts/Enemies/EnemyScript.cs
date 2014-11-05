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
	public bool CanAttack = true;
	public CharacterHealth ch;
	double attackTime = 0.0;
	double AttackDelay = 1.0;
	public AudioClip jab;
	
	void Start() 
	{
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
		
		//if the distance gets within the chaseRange the follower will start following the player
		if (distance <= chaseRange) {
			
			float z = Mathf.Atan2 ((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
			
			transform.eulerAngles = new Vector3 (0, 0, z);
			
			rigidbody2D.AddForce (gameObject.transform.up * speed);
			
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
	
	
}
