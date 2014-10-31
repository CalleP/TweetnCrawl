using UnityEngine;
using System.Collections;
using System;


public class Room : MonoBehaviour {


    int x1;
    int x2;
    int y1;
    int y2;

    int width;
    int height;

    Vector2 center;

    GameObject room;

    public Room(int x, int y, int width, int height) {
        this.width = width;
        this.height = height;

        x1 = x;
        y1 = y;
        x2 = x + width;
        y2 = y + height;

        center = new Vector2((float)Math.Floor(((double)x1 + (double)x2) / (double)2),
            (float)Math.Floor(((double)y1 + (double)y2) / (double)2));
    
    }

    public bool collides(Room room) {
        return false;
    }
	// Use this for initialization
	void Start () {
        //room = Resources.Load("Tile");
        //room = Instantiate()
	}
	
	// Update is called once per frame
	void Update () {
	   
	}
}
