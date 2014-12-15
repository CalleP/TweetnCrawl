using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoreExplosion : MonoBehaviour {

	// Use this for initialization

    public List<Sprite> GoreGibs;
	void Start () {
        var CreatedGore = new List<Collider2D>();
        foreach (var gib in GoreGibs)
        {
            var gore = (GameObject)Instantiate(Resources.Load("Gib"), transform.position, Quaternion.identity);
            gore.GetComponent<SpriteRenderer>().sprite = gib;
            transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0,360)));
            gore.rigidbody2D.AddForce(transform.up * UnityEngine.Random.Range(1000f,2500f));
            gore.transform.rotation = transform.rotation;
            gore.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));
            foreach (var collider in CreatedGore)
            {
                Physics2D.IgnoreCollision(gore.collider2D, collider);
            }
            CreatedGore.Add(gore.collider2D);


        }
	}
}
