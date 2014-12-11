using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoreExplosion : MonoBehaviour {

	// Use this for initialization

    public List<Sprite> GoreGibs;
	void Start () {
        foreach (var gib in GoreGibs)
        {
            var gore = (GameObject)Instantiate(Resources.Load("Gib"), transform.position, Quaternion.identity);
            gore.GetComponent<SpriteRenderer>().sprite = gib;
            transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0,360)));
            gore.rigidbody2D.AddForce(transform.up * UnityEngine.Random.Range(1000f,2500f));
            gore.transform.rotation = transform.rotation;
            gore.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));
            //Physics2D.IgnoreCollision(shell.collider2D, player.collider2D);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
