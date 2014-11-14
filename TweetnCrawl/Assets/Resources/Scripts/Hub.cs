using UnityEngine;
using System.Collections;

public class Hub :TileMap {


    public string Username;
    public string UserID;

    public string[] friends;

    public bool test = false;

    public TileMap NorthMap;
    public TileMap WestMap;
    public TileMap SouthMap;
    public TileMap EastMap;
    public TileMap CenterMap;

    public TileStruct[][] FullMap;

    public bool WestToNorth;  
    public bool SouthToWest;
    public bool EastToSouth;

    public bool started = false;

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




        newHub();


        //MergeHorizontal(WestMap.map, CenterMap.map);

        //MergeHorizontal(map, EastMap.map);

        //MergeVertical(map, NorthMap.map);

        //MergeVertical(SouthMap.map, map);

        //MergeVertical(CenterMap.map, NorthMap.map);

        //MergeVertical(SouthMap.map, map);





        //CenterMap = CropMap2(map, WestMap.Width,0,)


        //DrawCorridorHVertical(0, 10, 5, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.BlackCaste);



        //Debug.Log(CenterMap.TilesBeside(0,WestMap.EndPointY,direction.right,TileType.Rock));

        MergeAll();
        started = true;

        //DrawMap(arr);



        ObjectPlacer.testStart();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (Input.GetKey(KeyCode.K))
        {
                Copy(WestMap.map, EastMap.map);
           
                
                
                Copy(EastMap.map, EastMap.createMap());

                newHub();

        }


            
            
     }




    public void newHub()
    {


        int count = 0;
        while (true)
        {
            var mapGen = new MapHandler(CenterMap.Width, CenterMap.Height, 48);
            var arr = mapGen.createMap();

            Copy(CenterMap.map, arr);

       

            CenterMap.TrimMap();
            CenterMap.TrimMap();
            CenterMap.TrimMap();
            CenterMap.TrimMap();
            CenterMap.TrimMap();
            
            CenterMap.PlaceBorders(TileType.Rock);





            var NorthPoint = CenterMap.map[CenterMap.Height - 1][NorthMap.StartPointX];
            var SouthPoint = CenterMap.map[0][SouthMap.EndPointX];
            var WestPoint = CenterMap.map[WestMap.EndPointY][0];
            var EastPoint = CenterMap.map[EastMap.StartPointY][CenterMap.Width - 1];
            if (started)
            {
                NorthPoint = CenterMap.map[CenterMap.Height - 1][NorthMap.StartPointX];
                SouthPoint = CenterMap.map[0][SouthMap.EndPointX];
                WestPoint = CenterMap.map[WestMap.EndPointY][0];
                EastPoint = CenterMap.map[EastMap.StartPointY][CenterMap.Width - 1];
            }



    

            //Draw Corridor connecting centerMap and EastMap
            CenterMap.DrawCorridorHorizontal(
                (CenterMap.Width) - CenterMap.TilesBeside(CenterMap.Width, EastMap.StartPointY, direction.left, TileType.Rock),
                CenterMap.Width,
                EastMap.StartPointY,
                TileType.Rock,
                TileType.Dirt,
                TerrainType.YellowCave,
                TerrainType.YellowCave);

            //Draw Corridor connecting centerMap and SouthMap
            CenterMap.DrawCorridorHVertical(
                0,
                CenterMap.TilesBeside(SouthMap.EndPointX, 0, direction.up, TileType.Rock),
                SouthMap.EndPointX,
                TileType.Rock,
                TileType.Dirt,
                TerrainType.YellowCave,
                TerrainType.YellowCave);

            MapChecker checker = new MapChecker(CenterMap);
            EastToSouth = checker.CheckMap(SouthPoint, EastPoint, direction.up);


            //Draw Corridor connecting centerMap and WestMap
            CenterMap.DrawCorridorHorizontal(
                0,
                CenterMap.TilesBeside(0, WestMap.EndPointY, direction.right, TileType.Rock),
                WestMap.EndPointY,
                TileType.Rock,
                TileType.Dirt,
                TerrainType.YellowCave,
                TerrainType.YellowCave);

            checker = new MapChecker(CenterMap);
            SouthToWest = checker.CheckMap(SouthPoint, WestPoint, direction.right);


            //Draw Corridor connecting centerMap and NorthMap
            CenterMap.DrawCorridorHVertical(
                (CenterMap.Height) - CenterMap.TilesBeside(NorthMap.StartPointX, CenterMap.Height, direction.down, TileType.Rock),
                CenterMap.Height,
                NorthMap.StartPointX,
                TileType.Rock,
                TileType.Dirt,
                TerrainType.YellowCave,
                TerrainType.YellowCave);

            checker = new MapChecker(CenterMap);
            WestToNorth = checker.CheckMap(WestPoint, NorthPoint, direction.right);

            if ((WestToNorth && SouthToWest && EastToSouth)||count > 5)
            {
                break;
            }

            count++;
        }
    
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






    public void MergeAll()
    {
        var outArr = new TileStruct[NorthMap.Height+CenterMap.Height+SouthMap.Height][];
        for (int i = 0; i < outArr.Length; i++)
		{
            outArr[i] = new TileStruct[WestMap.Width + CenterMap.Width + SouthMap.Height];
            for (int i2 = 0; i2 < outArr[i].Length; i2++)
            {
                outArr[i][i2] = new TileStruct(i2, i, TileType.None);
            }
		}

        for (int y = 0; y  < WestMap.Height; y++)
        {
            for (int x = 0; x < WestMap.Width; x++)
            {
                var tile = WestMap.map[y][x];
                tile.Y = tile.Y + SouthMap.Height;
                outArr[y + SouthMap.Height][x] = tile;
                

            }
        }
        //WestMap.StartPointY = WestMap.StartPointY + SouthMap.Height;

        for (int y = 0; y < EastMap.Height; y++)
        {
            for (int x = 0; x < EastMap.Width; x++)
            {
                var tile = EastMap.map[y][x];
                tile.Y = tile.Y + SouthMap.Height;
                tile.X = tile.X + WestMap.Width + CenterMap.Width;
                outArr[y + SouthMap.Height][x+WestMap.Width+CenterMap.Width] = tile;


            }
        }
        //EastMap.StartPointX = WestMap + EastMap.Width;


        for (int y = 0; y < CenterMap.Height; y++)
        {
            for (int x = 0; x < CenterMap.Width; x++)
            {
                var tile = CenterMap.map[y][x];
                tile.Y = tile.Y + SouthMap.Height;
                tile.X = tile.X + WestMap.Width;
                outArr[y + SouthMap.Height][x + WestMap.Width] = tile;


            }
        }

        for (int y = 0; y < SouthMap.Height; y++)
        {
            for (int x = 0; x < SouthMap.Width; x++)
            {
                var tile = SouthMap.map[y][x];
                tile.X = tile.X + WestMap.Width;
                outArr[y][x + WestMap.Width] = tile;


            }
        }


        for (int y = 0; y < NorthMap.Height; y++)
        {
            for (int x = 0; x < NorthMap.Width; x++)
            {
                var tile = NorthMap.map[y][x];
                tile.X = tile.X + WestMap.Width;
                tile.Y = tile.Y + SouthMap.Height + CenterMap.Height;
                outArr[y + SouthMap.Height + CenterMap.Height][x + WestMap.Width] = tile;


            }
        }


        map = outArr;
        Height = map.Length;
        Width = map[0].Length;
    }


    public void Copy(TileStruct[][] original, TileStruct[][] newCopy)
    {
        for (int y = 0; y < original.Length; y++)
        {
            for (int x = 0; x < original[0].Length; x++)
            {
                var tile = newCopy[y][x];
                original[y][x].Type = tile.Type;
                
            
            }
        }

        
    }



    public void SwapVertical()
    { 
    
    }

    public void GenerateHub()
    {

    
    }

}
