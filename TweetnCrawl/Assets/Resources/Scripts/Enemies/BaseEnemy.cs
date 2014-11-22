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
	double attackTime = 0.0; //Enemy attacktime
	double AttackDelay = 1.0; //Enemy attack cooldown
	float rotationSpeed = 50f; //The speed of which an enemy rotates when patrolling
	public float rotationTime = 5f; //The time it takes for the enemy to rotate
	public AudioClip jab; //The enemy's attack sound
	public float patrolspeed = 5; //The speed of which an enemy patrol at
	public bool waiting = false; //The boolean if the enemy has moved and is standing still true/false
	Vector3 randomPosition; //The position an enemy will randomly move to when patrolling
	Quaternion qTo; //Rotation reference
	public Transform Projectile;


	// Use this for initialization
	void Start () {

		player = GameObject.Find("Player").transform; //Reference and find the playerobject
		attackTime = Time.time; //Attacktime is set to actual time
		CharacterHealth ch = GameObject.Find ("Player").GetComponent<CharacterHealth> (); //Reference to the players health
		chaseRange = 30.0f; //Sets the chaserange
		speed = Random.Range(10,20); //Randomizes the enemy's speed of a value between 10-20
		Follower = transform; //References the Enemy
		PrevSpeed = speed; //Stores the speed if we need to resume that speed later.


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void receiveDamage (int dmg) {
		health = health - dmg;
		Debug.Log("Recieved this amount of damage "+dmg.ToString()+" now health="+health.ToString() );
	}

	public void EnemyAttack() {
		
		Instantiate(Projectile, transform.position + (player.position - transform.position).normalized, transform.rotation);    
			
	}


}
