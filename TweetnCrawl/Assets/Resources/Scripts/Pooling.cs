using UnityEngine;
using System.Collections;


/// <summary>
///  This class controls how far the map will bbe rendered to allow for bigger maps at higher frames per second.
/// </summary>
public class Pooling : MonoBehaviour {


    GameObject[][] obj;
    //In pixels
    public static int TileWidth = 320;
    //In pixels
    public static int TileHeight = 320;

    public TileMap map;

    public int CenterPointX = 5;
    public int CenterPointY = 5;

    public int ViewPortHeight = 5;
    public int ViewPortWidth = 5;


    

    //TODO REPLACE 3.2 AND 100 WITH SOMETHING LESS STUPID
	void Start () {

        

        //creates a 2D array from the map 
        var StartingViewPort = 
            TileMap.CropMap(map.map, CenterPointX - ((ViewPortWidth / 2) + ViewPortWidth % 2), CenterPointY - ((ViewPortHeight / 2) + ViewPortHeight % 2), CenterPointX + (ViewPortWidth / 2), CenterPointY + (ViewPortHeight / 2));

        obj = new GameObject[StartingViewPort.Length][];
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i] = new GameObject[StartingViewPort[i].Length];
        }

        var prefab = Resources.Load("Tile");
        int count = 0;

        foreach (var item in StartingViewPort)
        {
            int count2 = 0;
            foreach (var item2 in item)
            {
                var tile = (GameObject)Instantiate(prefab);
                var script = (Tile)tile.GetComponent<Tile>();
                
                script.TileData = item2;
                tile.transform.parent = transform;
                //100 should be replaced with something scaleable
                tile.transform.position = new Vector3((float)item2.X * ((float)TileWidth / 100), (float)item2.Y * ((float)TileHeight / 100));

                obj[count][count2] = tile;
                count2++;
            }
            count++;
        }
    }

    void Update()
    {

        var test = TileMap.CropMap(map.map,CenterPointX - ((ViewPortWidth / 2) + ViewPortWidth % 2), CenterPointY - ((ViewPortHeight / 2) + ViewPortHeight % 2), CenterPointX + (ViewPortWidth / 2), CenterPointY + (ViewPortHeight / 2));

        //TODO This is inefficent, change
        GameObject player = GameObject.Find("Player");
        MoveToMapPos(((int)(player.transform.position.x / 3.2f)) , (int)(player.transform.position.y / 3.2f));
        
    }



    /// <summary>
    /// Moves the center of the Viewport to the specified map coordinates
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    public void MoveToMapPos(int x, int y)
    {
        int distanceX = x - CenterPointX;
        int distanceY = y - CenterPointY;

        if (CenterPointX != x)
        {
            if (distanceX > 0) StepRight();
            else StepLeft();
        }
        if (CenterPointY != y)
        {
            if (distanceY > 0) StepUp();
            else StepDown();
        }
    }


    /// <summary>
    /// Move the Viewport one tile to the right
    /// </summary>
    public void StepRight() 
    {
        var rightMostTile = obj[obj.Length-1][obj[0].Length-1].GetComponent<Tile>();
        if (rightMostTile.TileData.X < map.Width-1)
        {
            foreach (var item in obj)
            {
                foreach (var item2 in item)
                {
                    item2.GetComponent<Tile>().MoveHorizontally(1);
                }
            }
            CenterPointX++;
        }
    }

    /// <summary>
    /// Move Viewport one tile to the left
    /// </summary>
    public void StepLeft()
    {
        var leftMostTile = obj[0][0].GetComponent<Tile>();
        if (leftMostTile.TileData.X > 0)
        {
            foreach (var item in obj)
            {

                foreach (var item2 in item)
                {
                    item2.GetComponent<Tile>().MoveHorizontally(-1);
                }
            }
            CenterPointX--;
        }
    }

    /// <summary>
    /// Move the Viewport one tile up
    /// </summary>
    public void StepUp()
    {
        var upperMostTile = obj[obj.Length-1][0].GetComponent<Tile>();
        if (upperMostTile.TileData.Y < map.Height-1)
        {
            foreach (var item in obj)
            {
                foreach (var item2 in item)
                {
                    item2.GetComponent<Tile>().MoveVertically(1);
                }
            }
            CenterPointY++;
        }
    }


    /// <summary>
    /// Move the Viewport one tile down
    /// </summary>
    public void StepDown()
    {
        var upperMostTile = obj[0][0].GetComponent<Tile>();
        if (upperMostTile.TileData.Y > 0){
            foreach (var item in obj)
            {
                foreach (var item2 in item)
                {
                    item2.GetComponent<Tile>().MoveVertically(-1);
                }
            }
            CenterPointY--;
        }
    }

    void OnGUI() {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0;
        //var mapPos = map.GetTileDataCoords(p.x, p.y);
        var rect = new Rect(10, 10, 100, 20);
        var rect2 = new Rect(10, 30, 1000, 20);
        //GUI.Label(rect, mapPos.X.ToString() + ",  " + mapPos.Y.ToString());
        GUI.Label(rect2, p.x/3.2 + ",  " + p.y/3.2);
    }
}
