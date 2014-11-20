using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	public float speed; //The speed of the enemy
	public float chaseRange; //The range which the enemy chases the player
	public Transform player; //Reference to the player
	public Transform Follower; //Reference to the Enemy
	public float distance; //The distance between the player and the enemy
	public int health = 100; //The health of the enemy
	public float PrevSpeed; // stores the speed when speed is set to 0 and when you wish to return, prevspeed keeps the original speed given to the enemy
	public CharacterHealth ch; //Reference to the player's health
	double attackTime = 0.0; // Attacktime of the enemy
	double AttackDelay = 1.0; // The cooldown of enemy's attack
	public AudioClip jab; 
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
