using UnityEngine;
using System.Collections;

public class NewTile : MonoBehaviour {

    public static TileMap map;
	// Use this for initialization
    public int x;
    public int y;

    public TileStruct TileData;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TileData = map.GetTileData(TileData.X,TileData.Y);
        x=TileData.X;
        y = TileData.Y;

        transform.position = new Vector3(TileData.X * 3.2f, TileData.Y * 3.2f);

        if (TileData.Type == TileType.Dirt)
        {
            //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = SpriteHandler.GetTexture(TileData, map);//SpriteHandler.GetTexture(TileData, map.map);
            gameObject.tag = "Tile";
            //gameObject.GetComponent<SpriteRenderer>().sprite = dirt;
        }
        else if (TileData.Type == TileType.Rock)
        {

            //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = SpriteHandler.GetTexture(TileData, map);
            gameObject.tag = "Wall";
            //gameObject.GetComponent<SpriteRenderer>().sprite = rock;
        }
        else if (TileData.Type == TileType.Wood)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Rock");
        }
        else
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }

	}
}
