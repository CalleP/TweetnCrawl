using UnityEngine;
using System.Collections;

public class RoadSignBehaviour : MonoBehaviour {

	// Use this for initialization
    public string text;
	void Start () {
        transform.GetChild(0).GetComponent<TextMesh>().text = text.Trim();
	}
	
	// Update is called once per frame
	void Update () {




        //Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
        //var hitTarget = Physics2D.OverlapCircle(p, 0.1f);
        //if (hitTarget.gameObject == gameObject)
        //{
        //    Debug.Log("Hit");
        //}
        
	}


    


    void OnMouseEnter() 
    {	     


        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
    
    }
    void OnMouseExit()
    {


        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }
}
