using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

    public float duration = 1;
    float timeStamp;
	void Start () {
	    timeStamp = Time.time + duration;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeStamp <= Time.time)
        {
            Destroy(gameObject);
        }

	}
}
