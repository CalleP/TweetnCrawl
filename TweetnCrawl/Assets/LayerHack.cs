using UnityEngine;
using System.Collections;

public class LayerHack : MonoBehaviour {

    public int SortingLayerID;
    public int SortingOrder;
	
	void Update () {
        renderer.sortingLayerID = SortingLayerID;
        renderer.sortingOrder = SortingOrder;
	}
}
