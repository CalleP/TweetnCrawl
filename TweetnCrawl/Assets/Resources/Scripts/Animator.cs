using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Animator : MonoBehaviour {


    
    public bool loop;
    public float interval;
    public List<Sprite> sprites = new List<Sprite>();
    public bool DeathAfterLastFrame;
    public bool PlayOnAwake = true;

    private SpriteRenderer sr;
    private int index;
    private float time;


    public List<List<Sprite>> specialFrames = new List<List<Sprite>>();

    public virtual void Start () {
        
        time = Time.time;
        index = 0;
        sr = gameObject.GetComponent<SpriteRenderer>();
        try
        {
            sr.sprite = sprites[index];
        }
        catch (System.Exception)
        {
            
            throw;
        }

    }
	
    void FixedUpdate()
    {
        if (PlayOnAwake)
        {
            if (time <= Time.time)
            {
                if ((index >= sprites.Count - 1) && loop)
                {
                    index = 0;
                    sr.sprite = sprites[index];

                }
                else if (index >= sprites.Count - 1 && DeathAfterLastFrame)
                {
                    sr.sprite = null;
                    Destroy(gameObject);
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
}
