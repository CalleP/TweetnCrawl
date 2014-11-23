using UnityEngine;
using System.Collections;

public class EnemyScript : BaseEnemy {


	void Start() 
	{
		baseReferences ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		Flip ();
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

			chase();
			//if the enemy is close enough with a distance of 5 or less hit the player.
			if (distance <= chaseRange - 25f) {
				speed = 0;
				
				// a simple boolean checking if the enemy can attack or not to provide delay
				if (Time.time > attackTime && GameObject.Find("Player").GetComponent<CharacterHealth>().health >= 0) {

					meleeAttack();
					attackTime = Time.time + AttackDelay;
					
				} else {
					speed = PrevSpeed;
				}
				
			} else {
				return;
			}
		} 
	}

}
