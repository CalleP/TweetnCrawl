﻿using UnityEngine;
using System.Collections;

/// <summary>
/// The representation of a tile displayed, 
/// should only be used within the viewport, initializing many of these can cause a big performance hit.
/// </summary>
public class Tile : MonoBehaviour {

    public TileStruct TileData;
    public bool CollidingWithPlayer = false;
    public static Sprite dirt = Resources.Load<Sprite>("Minecraft_dirt");
    public static Sprite rock = Resources.Load<Sprite>("rock");
    private static TileMap map = GameObject.Find("Map").GetComponent<TileMap>();

    //TODO: add texture and stuff later
    public Tile(int x, int y) {
        TileData.X = x;
        TileData.Y = y;
    }

	void Start () {
       
        gameObject.name = TileData.X + "," + TileData.Y;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = dirt;

        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.size = sr.sprite.bounds.size;
	}

    void Update() 
    {
        //TileData = map.GetTileData(TileData.X, TileData.Y);

        if (transform.position.x != TileData.X*3.2)
        {
            transform.position = new Vector3(TileData.X * 3.2f, transform.position.y);
        }
        if (TileData.Type == TileType.Dirt)
        {

            gameObject.GetComponent<SpriteRenderer>().sprite = dirt;
        }
        else if (TileData.Type == TileType.Rock)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = rock;
        }
        else if (TileData.Type == TileType.Wood)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = rock;
        }
        else
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        CollidingWithPlayer = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        CollidingWithPlayer = false;
    }




    /// <summary>
    /// Moves the tile vertically.
    /// </summary>
    /// <param name="dir">The direction to move, Positive numbers move you down one tile, negative numbers move you up one tile.</param>
    public void MoveVertically(int dir)
    {
        transform.position = transform.position += new Vector3(0f, (float)Pooling.TileHeight / (float)(100 * dir / Mathf.Abs(dir)));
        TileData = map.GetTileData(TileData.X, TileData.Y + (dir / Mathf.Abs(dir)));
       
    }


    /// <summary>
    /// Moves the tile horizontally
    /// </summary>
    /// <param name="dir">The direction to move, Positive numbers move you to the right one tile, negative numbers move you left one tile.</param>
    public void MoveHorizontally(int dir)
    {
        transform.position = transform.position += new Vector3((float)Pooling.TileWidth / (float)(100 * dir / Mathf.Abs(dir)), 0f);
        TileData = map.GetTileData(TileData.X + (dir / Mathf.Abs(dir)), TileData.Y);
    }
}
