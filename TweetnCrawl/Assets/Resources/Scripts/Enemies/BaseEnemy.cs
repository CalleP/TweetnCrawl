using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

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
	public Transform Projectile;

	
	//Method containing the basereferences for the enemies
	public void baseReferences() {
		player = GameObject.Find("Player").transform; //Reference and find the playerobject
		attackTime = Time.time; //Attacktime is set to actual time
		CharacterHealth ch = GameObject.Find ("Player").GetComponent<CharacterHealth> (); //Reference to the players health
		chaseRange = 30.0f; //Sets the chaserange
		speed = Random.Range(10,20); //Randomizes the enemy's speed of a value between 10-20
		Follower = transform; //References the Enemy
		PrevSpeed = speed; //Stores the speed if we need to resume that speed later.

		}
	//Method dealing damage to the enemies
	public void receiveDamage (int dmg) {
		health = health - dmg;
		Debug.Log("Recieved this amount of damage "+dmg.ToString()+" now health="+health.ToString() );
	}

	//Shootattack for the enemies
	public void shootAttack() {
		
		Instantiate(Projectile, transform.position + (player.position - transform.position).normalized, transform.rotation);    
			
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
			print("stopped patrol");
			StopCoroutine (patrolUpdate ());
			StartCoroutine (patrolUpdate ());
		}
		else
		{
			
			transform.position = Vector3.Lerp (transform.position, randomPosition, Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime);
			
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

	//Chase method for the enemies
	public void chase() {
		float z = Mathf.Atan2 ((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
		
		transform.eulerAngles = new Vector3 (0, 0, z);
		
		rigidbody2D.AddForce(transform.up * speed);
	}
	


}
