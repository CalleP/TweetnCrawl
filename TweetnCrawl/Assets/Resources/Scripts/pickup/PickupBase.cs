using UnityEngine;
using System.Collections;

public class PickupBase : MonoBehaviour {

    public Object Item;


	
	// Update is called once per frame
	void Update () {
	
	}


    protected virtual void OnTriggerStay2D(Collider2D coll)
    {

    }
}
