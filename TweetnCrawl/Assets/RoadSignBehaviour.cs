using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadSignBehaviour : MonoBehaviour {

    public float fadeInSpeed = 0.1f;
    public float fadeOutSpeed = 0.01f;

	// Use this for initialization
    public string text;
	void Start () {
        var str = transform.GetChild(0).GetComponent<TextMesh>().text = text.Trim();
        transform.GetChild(0).GetComponent<TextMesh>().text = FirstLetterToUpper(str);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!mouseOver)
        {
            var outlines = transform.GetChild(0).transform.GetComponentsInChildren<TextMesh>();
            foreach (var item in outlines)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.g, item.color.a - fadeOutSpeed);

            }
        }
	}

    private string FirstLetterToUpper(string str)
    {
        if (str == null)
            return null;

        if (str.Length > 1)
            return "#" + char.ToUpper(str[0]) + str.Substring(1);

        return str.ToUpper();
    }

    bool mouseOver = false;
    void OnMouseOver() 
    {
        mouseOver = true;
        var outlines = transform.GetChild(0).transform.GetComponentsInChildren<TextMesh>(); 
        foreach (var item in outlines)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.g, item.color.a + fadeInSpeed);

        }
    }
    void OnMouseExit()
    {
        mouseOver = false;

    }



   
}
