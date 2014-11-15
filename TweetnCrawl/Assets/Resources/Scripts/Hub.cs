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



        SouthMap.map = newMap(SouthMap);
        NorthMap.map = newMap(NorthMap);


        EastMap.map = newMap(EastMap);

        WestMap.map = newMap(WestMap);

        CenterMap.map = newHub(CenterMap);

        //newHub();


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
            Copy(WestMap.map, newMap(WestMap));
            Copy(EastMap.map, newMap(EastMap));
            Copy(NorthMap.map, newMap(NorthMap));
            Copy(SouthMap.map, newMap(SouthMap));
            Copy(CenterMap.map, newHub(CenterMap));
                


                

        }


            
            
    }



    public void StepUp()
    {
        Copy(WestMap.map, newMap(WestMap));
        Copy(EastMap.map, newMap(EastMap));
        Copy(SouthMap.map, NorthMap.map);
        Copy(NorthMap.map, newMap(SouthMap));
        Copy(CenterMap.map, newHub(CenterMap));
    }

    public void StepDown()
    {
        Copy(WestMap.map, newMap(WestMap));
        Copy(EastMap.map, newMap(EastMap));
        Copy(NorthMap.map, SouthMap.map);
        Copy(SouthMap.map, newMap(SouthMap));
        Copy(CenterMap.map, newHub(CenterMap));
    }

    public void StepLeft()
    {

        //
    }

    public void StepRight()
    { 
    
    //
    }


    public TileStruct[][] newMap(TileMap map)
    {
        var gen = new MapHandler(map.Width, map.Height, 48);
        return gen.createMap(ref map.StartPointX, ref map.StartPointY, ref map.EndPointX, ref map.EndPointY);
    }

    public TileStruct[][] newHub(TileMap centerMap)
    {

        var gen = new MapHandler(centerMap.Width, centerMap.Height, 48);
        return gen.createHub(WestMap, EastMap, SouthMap, NorthMap);
    
    }




    //public void createMap(TileMap sideMap)
    //{


    //    while (true)
    //    {



    //        MapHandler maphandler = new MapHandler(sideMap.Width, sideMap.Height, 48);

    //        var convertedArray = maphandler.createMap();

    //        Copy(sideMap.map, convertedArray);

    //        sideMap.TrimMap();
    //        sideMap.TrimMap();
    //        sideMap.TrimMap();
    //        sideMap.TrimMap();
    //        sideMap.TrimMap();

    //        sideMap.PlaceBorders(TileType.Rock);



    //        if (Width >= Height)
    //        {



    //            var closest = sideMap.ClosestToBorderX(TileType.Dirt);
    //            sideMap.DrawCorridorHorizontal(0, closest.X, closest.Y, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.BlackCaste);

    //            sideMap.StartPointX = 0;
    //            sideMap.StartPointY = closest.Y;


    //            var closest2 = sideMap.ClosestToBorderXReverse(TileType.Dirt);
    //            //SpawnStartExitPoint(ClosestToBorderX(TileType.Dirt), false);
    //            sideMap.DrawCorridorHorizontal(sideMap.map[0].Length, closest2.X, closest2.Y, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.BlackCaste);

    //            sideMap.EndPointX = map[0].Length - 1;
    //            sideMap.EndPointY = closest2.Y;

    //            MapChecker checker = new MapChecker(sideMap);


                
                
    //            if (started)
    //            {

    //                //Closest.X = NorthPoint.X - WestMap.Width;
    //                //NorthPoint.Y = NorthPoint.Y - SouthMap.Height;


                    

    //            }

    //            if (checker.CheckMap(closest, closest2, direction.right))
    //            {
    //                break;
    //            }

    //        }
    //        else
    //        {
    //            var closest = ClosestToBorderY(TileType.Dirt);
    //            DrawCorridorHVertical(0, closest.Y, closest.X, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.BlackCaste);

    //            StartPointX = closest.X;
    //            StartPointY = 0;


    //            var closest2 = ClosestToBorderYReverse(TileType.Dirt);
    //            //SpawnStartExitPoint(ClosestToBorderX(TileType.Dirt), false);
    //            DrawCorridorHVertical(map.Length, closest2.Y, closest2.X, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.BlackCaste);

    //            EndPointX = closest2.X;
    //            EndPointY = map.Length - 1;

    //            MapChecker checker = new MapChecker(this);

    //            if (checker.CheckMap(closest, closest2, direction.right))
    //            {
    //                break;
    //            }
    //        }


    //    }

    //    if (gameObject.name == "NorthMap")
    //    {
    //        Debug.Log("here");
    //    }
  
    //}




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


    //public void Copy(TileStruct[][] original, TileStruct[][] newCopy)
    //{
    //    for (int y = 0; y < original.Length; y++)
    //    {
    //        for (int x = 0; x < original[0].Length; x++)
    //        {
    //            var tile = newCopy[y][x];
    //            original[y][x].Type = tile.Type;
                
            
    //        }
    //    }

        
    //}



    public void SwapVertical()
    { 
    
    }

    public void GenerateHub()
    {

    
    }

}
