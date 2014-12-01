using UnityEngine;
using System.Collections;

public class ObjectPlacer : MonoBehaviour {


    //TODO: class sf not done. it was hastily scrounged together to make the demo look nicer

    private static System.Random rand = new System.Random();
    private static TileMap map = GameObject.Find("Hub").GetComponent<TileMap>(); 
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static TileStruct findAvailableTile()
    {
        return findAvailableTile(map, 0, 0);
            
        
    }

    public static TileStruct findAvailableTile(TileMap map)
    {
        return findAvailableTile(map, 0, 0);
    }


    public static TileStruct findAvailableTile(TileMap map, int Xoffset, int Yoffset)
    {
        TileStruct tile = new TileStruct(0,0,TileType.None);
        while (tile.Type != TileType.Dirt)
	    {

            tile = map.GetTileData(rand.Next(0, map.map[0].Length - 1) + Xoffset, rand.Next(0, map.map.Length - 1) + Yoffset);
	    }

        return tile;
    }

    public static TileStruct findAvailableCloseToPlayer(int maxDistance)
    {
        GameObject player = GameObject.Find("Player");
        var playerTile = map.GetTileData((int)(player.transform.position.x / 3.2f), (int)(player.transform.position.y / 3.2f));
        var tile = map.GetTileData(playerTile.X + Random.Range(-maxDistance, maxDistance), playerTile.Y + Random.Range(-maxDistance, maxDistance));
        while (tile.Type != TileType.Dirt)
        {
            tile = map.GetTileData(playerTile.X + Random.Range(-maxDistance, maxDistance), playerTile.Y + Random.Range(-maxDistance, maxDistance));
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

        Instantiate(Resources.Load("Enemy"), new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -0.15f), Quaternion.identity);
        Instantiate(Resources.Load("Enemy2"), new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -0.15f), Quaternion.identity);
        var obj = (GameObject)Instantiate(Resources.Load("BasicEnemy"), new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -0.15f), Quaternion.identity);
        obj.GetComponent<EnemyRandomizer>().RandomizeFrames(EnemyTypes.Basic, TerrainType.BlackCaste);


        Instantiate(Resources.Load("Splitter"), new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -0.15f), Quaternion.identity);
        //Instantiate(Resources.Load("RangedSplitter"), new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -1f), Quaternion.identity);
        Instantiate(Resources.Load("RangedSplitter"), new Vector3(385, 385, -0.15f), Quaternion.identity);
        Instantiate(Resources.Load("HiveEnemy"), new Vector3(385, 385, -0.15f), Quaternion.identity);
        Instantiate(Resources.Load("TeleporterEnemy"), new Vector3(385, 385, -0.15f), Quaternion.identity);

    }

    public static void testStart()
    {
        //spawnEnemy ();



            var selectectedMap = GameObject.Find("Hub").GetComponent<Hub>().CenterMap;
            var checker = new MapChecker(selectectedMap.map);

            var tile = findAvailableTile(selectectedMap);

            //if (checker.CheckMap(selectectedMap.,))
            //{
                
            //}




        var height = (map.Height / 2) * 3.2f;
        var width = (map.Width / 2) * 3.2f;
        GameObject.Find("Player").transform.position = new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -1);
        GameObject.Find("Pickup1").transform.position = new Vector3(width - 3.2f, height, -1);
        GameObject.Find("Pickup2").transform.position = new Vector3(width + 3.2f, height, -1);
        GameObject.Find("Pickup3").transform.position = new Vector3(width, height + 3.2f, -1);
    }


    public static void spawnPickup(BaseWeapon pickup)
    { 
        
    }

    
    
}
