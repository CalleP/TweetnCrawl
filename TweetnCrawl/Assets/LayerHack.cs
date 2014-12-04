using UnityEngine;
using System.Collections;

public class LayerHack : MonoBehaviour {

    public int SortingLayerID;
    public int SortingOrder;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        renderer.sortingLayerID = SortingLayerID;
        renderer.sortingOrder = SortingOrder;
        //GetComponent<TextMesh>().sor
        //GetComponent().sortingLayerID = blah;
        //and.sortingOrder = blah;
	}
}
