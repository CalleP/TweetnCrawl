using UnityEngine;
using System.Collections;

public class BloodFlow : MonoBehaviour {
	
	// Update is called once per frame
    public Material BloodMaterial;
    public Color BloodColor;
    Vector3 oldPos;

    float time;
    public float TimeBetweenSplash = 0.1f;
	void Update () {
        if (transform.position != oldPos && time <= Time.time)
        {
            var bloodSpot = (GameObject)Instantiate(gameObject);
            bloodSpot.rigidbody2D.isKinematic = true;
            bloodSpot.collider2D.enabled = false;
            bloodSpot.renderer.material = BloodMaterial;
            bloodSpot.GetComponent<SpriteRenderer>().color = BloodColor;
            bloodSpot.GetComponent<BloodFlow>().enabled = false;
            bloodSpot.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.01f);
            //bloodSpot.transform.parent = gameObject.transform;
            time = Time.time + TimeBetweenSplash;

        }
        oldPos = transform.position;
	}
}
