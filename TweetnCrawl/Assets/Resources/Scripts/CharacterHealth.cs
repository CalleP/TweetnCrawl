using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {
	
	public int health = 100;
	public Transform Player;
	
	
	
	// Use this for initialization
	void Start () {
		
		Player = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			//no damage method is implemented yet so the enemy cannot die as of now.
			print("Blaaah you killed me!");
			PlayerDeath();
			
		}
		
	}
	
	public void receiveDamage (int dmg) {
		health = health - dmg;
		Debug.Log("Recieved this amount of damage "+dmg.ToString()+" now health="+health.ToString() );
	}
	
	public void PlayerDeath () {
		
		Vector3 pos = Player.transform.position;
		Instantiate(Resources.Load("BlueExplosion"), pos, Quaternion.identity);
	}
}
