using UnityEngine;
using System.Collections;

public class PickupBase : MonoBehaviour {

    public Object Item;
	// Use this for initialization



	
	// Update is called once per frame
	void Update () {
	
	}


    protected virtual void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            coll.gameObject.GetComponent<Inventory>().PickUp(this);
            Destroy(gameObject);
        }
    }
}
