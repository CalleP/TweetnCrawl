using UnityEngine;
using System.Collections;

public class BloodFlow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    public Material BloodMaterial;
    public Color BloodColor;
    Vector3 oldPos;
	void Update () {
        if (transform.position != oldPos)
        {
            var bloodSpot = (GameObject)Instantiate(gameObject);
            bloodSpot.rigidbody2D.isKinematic = true;
            bloodSpot.collider2D.enabled = false;
            bloodSpot.renderer.material = BloodMaterial;
            bloodSpot.GetComponent<SpriteRenderer>().color = BloodColor;
            bloodSpot.GetComponent<BloodFlow>().enabled = false;
            bloodSpot.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.01f);
        }
        oldPos = transform.position;
	}
}
