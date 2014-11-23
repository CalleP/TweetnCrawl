﻿using UnityEngine;
using System.Collections;

public class EnemyScript2 : BaseEnemy {

	public Sprite IdleState1; //Enemy idlestate1
	public Sprite IdleState2; //Enemy idleState2
	
	void Start() 
	{
		baseReferences ();
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
			
			chase();
			//if the enemy is close enough with a distance of 5 or less hit the player.
			if (distance < chaseRange - 10) {
				speed = 0;
				
				// a simple boolean checking if the enemy can attack or not to provide delay
				if (Time.time > attackTime && GameObject.Find ("Player").GetComponent<CharacterHealth> ().health >= 0 && distance <= chaseRange - 10) {
					patrol();
					shootAttack ();
					attackTime = Time.time + AttackDelay;
					
				} 
				
			} else if (distance > chaseRange - 10) {
				speed = PrevSpeed;
				return;
			}
		} 
	}
	
	
}
