using UnityEngine;
using System.Collections;

public class Hub :TileMap {


    public string Username;
    public string UserID;

    public string[] friends;



    public TileMap NorthMap;
    public TileMap WestMap;
    public TileMap SouthMap;
    public TileMap EastMap;

    public TileStruct[][] FullMap;
    public TileStruct[][] CenterMap;

	public override void Start () {
        
        base.Start();


        CenterMap = map;


        ObjectPlacer.testStart();


        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();
        //ObjectPlacer.spawnEnemy();


        var obj = Resources.Load("TemporaryPrefabs/smallRocks");

        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);


        obj = Resources.Load("TemporaryPrefabs/smallRocks2");

        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);



        obj = Resources.Load("TemporaryPrefabs/Bones1");

        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);
        ObjectPlacer.spawnObject(obj);


        //CenterMap = new TileStruct[WestMap.Height][];
        //for (int i = 0; i < WestMap.Height; i++)
        //{
        //    CenterMap[i] = new TileStruct[SouthMap.Width];
        //    for (int y = 0; y < CenterMap[0].Length; y++)
        //    {
        //        CenterMap[i][y] = new TileStruct(y, i, TileType.Dirt);
        //    }
        //}
        
        
        //map = CenterMap;
        MergeHorizontal(WestMap.map,map);
        Debug.Log("created center map bounds");


        MergeHorizontal(map, EastMap.map);

        //MergeVertical(map, NorthMap.map);

        //MergeVertical(SouthMap.map, map);


        DrawCorridorHVertical(0, 10, 5, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.BlackCaste);


        DrawCorridorHorizontal(95, 110, WestMap.EndPointY, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.BlackCaste);

        DrawCorridorHorizontal(125, 140, EastMap.StartPointY, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.BlackCaste);

        //DrawMap(map);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //public void SetCenter();

    //public void SetNorth(int userID);

    //public void SetSouth(int userID);
    //public void SetWest(int userID);
    //public void SetEast(int userID);


    //public void MergeAll();
    public void MergeHorizontal(TileStruct[][] map2, TileStruct[][] map1) {
        var outArr = new TileStruct[map1.Length][];
        for (int i = 0; i < map1.Length; i++)
        {
            outArr[i] = new TileStruct[map1[0].Length + map2[0].Length];
        }

        for (int y = 0; y < outArr.Length; y++)
        {
            for (int x = 0; x < outArr[0].Length; x++)
            {
                if (x < map2[0].Length)
	            {
                    outArr[y][x] = map2[y][x];
	            }
                else
                {
                    var tile = map1[y][x - map2[0].Length];
                    var newTile = new TileStruct(x, tile.Y, tile.Type);
                    outArr[y][x] = newTile;
                }
            }
        }
        map = outArr;
        Height = map.Length;
        Width = map[0].Length;
    
    }

    public void MergeVertical(TileStruct[][] map2, TileStruct[][] map1)
    {

        var outArr = new TileStruct[map2.Length + map1.Length][];
        for (int i = 0; i < outArr.Length; i++)
        {
            outArr[i] = new TileStruct[Width];
        }

        int count = 0;
        for (int i = 0; i < map2.Length; i++)
        {
            outArr[i] = map2[i];
            count++;
        }

        for (int i = 0; i < map1.Length; i++)
        {
            var tile = new TileStruct[map1[0].Length];
            for (int x = 0; x < tile.Length; x++)
            {
                var oldTile = map1[i][x];
                tile[x] = new TileStruct(oldTile.X, count + oldTile.Y, oldTile.Type);
            }

            outArr[i + count] = tile;


        }



        map = outArr;
       
        Height = map.Length;
        Width = map[0].Length;

       


    }


    public void MergeEast(){
    
    
    }
    //public void MergeNorth();
    //public void MergeSouth();

}
