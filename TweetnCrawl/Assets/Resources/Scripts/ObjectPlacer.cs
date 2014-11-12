using UnityEngine;
using System.Collections;

public class ObjectPlacer : MonoBehaviour {


    //TODO: class sf not done. it was hastily scrounged together to make the demo look nicer

    private static System.Random rand = new System.Random();
    private static TileMap map = GameObject.Find("CenterMap").GetComponent<TileMap>(); 
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static TileStruct findAvailableTile(){
        TileStruct tile = new TileStruct(0,0,TileType.None);
        while (tile.Type != TileType.Dirt)
	    {
	                 tile = map.map[rand.Next(0, map.map.Length-1)][rand.Next(0,map.map[0].Length-1)];
	    }

        return tile;
    }


    public static void spawnObject(Object obj)
    {
        var tile = findAvailableTile();
        Instantiate(obj, new Vector3(tile.X*3.2f,tile.Y*3.2f,-0.8f), Quaternion.identity);
    }

    public static void spawnEnemy(/*BaseEnemy enemy*/)
    {
        var tile = findAvailableTile();

        Instantiate(Resources.Load("Enemy"), new Vector3(tile.X*3.2f,tile.Y*3.2f,-1f), Quaternion.identity);

    }

    public static void testStart()
    {
        var tile = findAvailableTile();

        GameObject.Find("Player").transform.position = new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -1);
        GameObject.Find("Pickup1").transform.position = new Vector3((tile.X-1) * 3.2f, tile.Y * 3.2f, -1);
        GameObject.Find("Pickup2").transform.position = new Vector3((tile.X+1) * 3.2f, tile.Y * 3.2f, -1);
    }

    public static void spawnPickup(BaseWeapon pickup)
    { 
        
    }


    
}
