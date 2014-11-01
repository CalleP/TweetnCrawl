using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {

    public BaseWeapon weapon;
	// Use this for initialization
	void Start () {
        weapon = new ShotgunWeapon();
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            coll.gameObject.GetComponent<Inventory>().weapons.Add(weapon);
            coll.gameObject.GetComponent<Inventory>().EquipWeapon(0);
            Destroy(gameObject);
        }
    }
}
