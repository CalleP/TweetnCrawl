using UnityEngine;
using System.Collections;

public class EnemyScript : BaseEnemy {
	public float speed;
	public float chaseRange = 5.0f;
	public Transform player;
	public Transform Follower;
	public float distance;

	
	// Update is called once per frame
	void FixedUpdate () {


		//Updates constantly the distance between the follower and the player
		distance = Vector3.Distance( Follower.position,player.position );

		//if the distance gets within the chaseRange the follower will start following the player
		if (distance <= chaseRange) {

						float z = Mathf.Atan2 ((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

						transform.eulerAngles = new Vector3 (0, 0, z);

						rigidbody2D.AddForce (gameObject.transform.up * speed);

				} else {
						return; 
				}
		//TODO add attack method 

	}

	//subtracts the enemy health with the player damage.

}
