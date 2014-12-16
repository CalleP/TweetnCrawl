using UnityEngine;
using System.Collections;

public class LaserFade : MonoBehaviour {


    LineRenderer line;
	void Start () 
    {
        line = GetComponent<LineRenderer>();
	}

    public float width = 2f;
	void Update () 
    {

        if (width <= 0.1f)
        {
            Destroy(gameObject);
        }
      
	}

    void FixedUpdate()
    {

        line.SetWidth(width,width);
        width -= 0.1f;
        
    }
}
