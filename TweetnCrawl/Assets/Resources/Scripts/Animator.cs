using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Animator : MonoBehaviour {

	// Use this for initialization

    public bool loop;
    public float interval;
    public List<Sprite> sprites = new List<Sprite>();
    public bool DeathAfterLastFrame;

    private SpriteRenderer sr;
    private int index;
    private float time;
    void Start () {
        time = Time.time;
        index = 0;
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[index];
    }
	
	// Update is called once per frame
	void Update () {
        if (time <= Time.time)
        {
            if ((index >= sprites.Count-1) && loop)
            {
                index = 0;
                sr.sprite = sprites[index];

            }
            else if (index >= sprites.Count - 1 && DeathAfterLastFrame)
            {
                sr.sprite = null;
            }
            else if (!(index >= sprites.Count - 1))
            {
                index++;
                sr.sprite = sprites[index];
            }

            
            time = Time.time + interval;
        }
	}
}
