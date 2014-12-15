using UnityEngine;
using System.Collections;

/// <summary>
/// The representation of a tile displayed, 
/// should only be used within the viewport, initializing many of these can cause a big performance hit.
/// </summary>
public class Tile : MonoBehaviour {

    public string Visited;
    public int x;
    public int y;
    public TileType Type;
    public TileStruct TileData;
    public bool CollidingWithPlayer = false;
    public Sprite dirt = Resources.Load<Sprite>("Minecraft_dirt");
    public Sprite rock = Resources.Load<Sprite>("rock");
    private TileMap map;
    public int surroundingTiles = 0;

    public string TerrainType;

    public DecorType decor;

    //TODO: add texture and stuff later
    public Tile(int x, int y) {
        TileData = map.GetTileData(x, y);
    }

    private SpriteRenderer sr;
	void Start () {
       map = GameObject.Find("World").GetComponent<Pooling>().map;

        gameObject.name = TileData.X + "," + TileData.Y;
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = dirt;

	}

    void Update() 
    {
        if (Oldposition != transform.position)
        {
            TileData = map.GetTileData(TileData.X, TileData.Y);
            Type = TileData.Type;
            x = TileData.X;
            y = TileData.Y;    

            if (!ReferenceEquals(oldTileData, TileData))
            {
                if (TileData.Type == TileType.Dirt)
                {
                    sr.sprite = SpriteHandler.GetTexture(TileData, map);
                    gameObject.tag = "Tile";
                }
                else if (TileData.Type == TileType.Rock)
                {
                    sr.sprite = SpriteHandler.GetTexture(TileData, map);
                    gameObject.tag = "Wall";
                }
                else if (TileData.Type == TileType.Wood)
                {
                    sr.sprite = rock;
                }
                else
                {
                    sr.sprite = dirt;
                }
            }
        }
        oldTileData = TileData;
        Oldposition = transform.position;
    }
    private Vector3 Oldposition;
    private TileStruct oldTileData;

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
    public void MoveVertically(int dir, int times)
    {
        transform.position = transform.position += new Vector3(0f, (float)Pooling.TileHeight / (float)(100 * dir / Mathf.Abs(dir)))*times;
        TileData = map.GetTileData(TileData.X, TileData.Y + ((dir / Mathf.Abs(dir))* times));
        
       
    }


    /// <summary>
    /// Moves the tile horizontally
    /// </summary>
    /// <param name="dir">The direction to move, Positive numbers move you to the right one tile, negative numbers move you left one tile.</param>
    public void MoveHorizontally(int dir, int times)
    {
        transform.position = transform.position += new Vector3((float)Pooling.TileWidth / (float)(100 * dir / Mathf.Abs(dir)), 0f) * times;
        TileData = map.GetTileData(TileData.X + ((dir / Mathf.Abs(dir))*times), TileData.Y);
    }

        
    public void setType(TileType type)
    {
        map.map[TileData.Y][TileData.X].Type = type;
        TileData.Type = type;
    }

}
