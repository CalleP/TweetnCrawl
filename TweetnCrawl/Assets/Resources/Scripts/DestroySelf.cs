using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Destroy());
	}
	
	IEnumerator Destroy() {

		yield return new WaitForSeconds(2);
		Destroy (gameObject);
		}
}
