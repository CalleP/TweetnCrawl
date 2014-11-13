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
    public TileMap CenterMap;

    public TileStruct[][] FullMap;


    private int originalWidth;
    private int originalLength;


	public override void Start () {
        
        base.Start();




        


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



        CenterMap.DrawCorridorHorizontal(
            0,
            CenterMap.TilesBeside(0, WestMap.EndPointY, direction.right, TileType.Rock),
            WestMap.EndPointY,
            TileType.Rock,
            TileType.Dirt,
            TerrainType.BlackCaste,
            TerrainType.BlackCaste);


        CenterMap.DrawCorridorHorizontal(
            (CenterMap.Width) - CenterMap.TilesBeside(CenterMap.Width, EastMap.StartPointY, direction.left, TileType.Rock),
            CenterMap.Width,
            EastMap.StartPointY,
            TileType.Rock,
            TileType.Dirt,
            TerrainType.BlackCaste,
            TerrainType.BlackCaste);


        CenterMap.DrawCorridorHVertical(
            0,
            CenterMap.TilesBeside(SouthMap.EndPointX, 0, direction.up, TileType.Rock),
            SouthMap.EndPointX,
            TileType.Rock,
            TileType.Dirt,
            TerrainType.BlackCaste,
            TerrainType.BlackCaste);

        CenterMap.DrawCorridorHVertical(
            (CenterMap.Height) - CenterMap.TilesBeside(NorthMap.StartPointX, CenterMap.Height, direction.down, TileType.Rock),
            CenterMap.Height,
            NorthMap.StartPointX,
            TileType.Rock,
            TileType.Dirt,
            TerrainType.BlackCaste,
            TerrainType.BlackCaste);



        Debug.Log(CenterMap.TilesBeside(NorthMap.StartPointX, CenterMap.Height, direction.down, TileType.Rock));

        MergeHorizontal(WestMap.map, CenterMap.map);

        MergeHorizontal(map, EastMap.map);

        //MergeVertical(map, NorthMap.map);

        //MergeVertical(SouthMap.map, map);

        //MergeVertical(CenterMap.map, NorthMap.map);

        //MergeVertical(SouthMap.map, map);



        //CenterMap = CropMap2(map, WestMap.Width,0,)


        //DrawCorridorHVertical(0, 10, 5, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.BlackCaste);



        //Debug.Log(CenterMap.TilesBeside(0,WestMap.EndPointY,direction.right,TileType.Rock));

        Debug.Log("Y: "+SouthMap.ClosestToBorderY(TileType.Dirt).Y);
        Debug.Log("X: "+SouthMap.ClosestToBorderY(TileType.Dirt).X);


        Debug.Log("Y2: " + SouthMap.ClosestToBorderYReverse(TileType.Dirt).Y);
        Debug.Log("X2: " + SouthMap.ClosestToBorderYReverse(TileType.Dirt).X);
        //var arr = MergeAll();
        //DrawMap(arr);

        ObjectPlacer.testStart();
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
                    try
                    {
                        outArr[y][x] = map2[y][x];
                    }
                    catch (System.Exception)
                    {
                        
                        throw;
                    }

	            }
                else
                {
                    var tile = map1[y][x - map2[0].Length];
                    tile.X = x;
                    //var newTile = new TileStruct(x, tile.Y, tile.Type);
                    outArr[y][x] = tile;
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
                try
                {
                    var oldTile = map1[i][x];
                    oldTile.Y = count + oldTile.Y;
                    tile[x] = oldTile;
                }
                catch (System.Exception)
                {
                    
                    throw;
                }


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



    public TileStruct[][] MergeAll()
    {
        var outArr = new TileStruct[NorthMap.Height+CenterMap.Height+SouthMap.Height][];
        for (int i = 0; i < outArr.Length; i++)
		{
            outArr[i] = new TileStruct[WestMap.Width + CenterMap.Width + SouthMap.Height];
		}

        for (int y = 0; y < outArr.Length; y++)
        {
            for (int x = 0; x < outArr[0].Length; x++)
            {
                if (x >= WestMap.Width && x < WestMap.Width + CenterMap.Width)
                {
                    if (y >= SouthMap.Height && y < SouthMap.Height + CenterMap.Height)
                    {
                        outArr[y][x] = CenterMap.GetTileData2(x - WestMap.Width, y - SouthMap.Height);
                    }
                    else if (y < SouthMap.Height)
                    {
                        outArr[y][x] = SouthMap.GetTileData2(x-WestMap.Width, y);
                    }
                    else
                    {
                        outArr[y][x] = NorthMap.GetTileData2(x-WestMap.Width, y-(SouthMap.Height+CenterMap.Height));
                    }
                }
                else if (x < WestMap.Width)
                {
                    outArr[y][x] = WestMap.GetTileData2(x, y);
                }
                else
                {
                    outArr[y][x] =  EastMap.GetTileData2(x-(WestMap.Width+CenterMap.Width), y);
                }
            }

            
        }

        return outArr;
    }

}
