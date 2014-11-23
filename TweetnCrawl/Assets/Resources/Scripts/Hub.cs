using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime;

public class Hub :TileMap {





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


    private float loadingTimer = 5f;

    public bool MainHub;

	public override void Start () {

        if (!MainHub)
        {
            base.Start();
        }


        Hashtag = "Koala";

        NorthMap.Hashtag = "KKKKK";

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



        if (MainHub)
        {
            PreInstantiateAll();
        }

        

        ObjectPlacer.testStart();



	}
	
	// Update is called once per frame

    private float time;
    public GameObject player;
    
    void Update () 
    {

        if (transition)
        {
                    RelocateAll(transitionDir);        
                    transition = false;

                    switch (transitionDir)
                    {
                        case direction.up:
                            NorthMap.PreInstantiateColliders();
                            break;
                        case direction.down:
                            SouthMap.PreInstantiateColliders();
                            break;
                        case direction.left:
                            WestMap.PreInstantiateColliders();
                            break;
                        case direction.right:
                            EastMap.PreInstantiateColliders();
                            break;
                        default:
                            break;
                    }

        }

        if (mapGenComplete)
        {

            PreInstantiateAll();
            mapGenComplete = false;



        }

        var playerX = TileStruct.UnityUnitToTileUnit(player.transform.position.x);
        var playerY = TileStruct.UnityUnitToTileUnit(player.transform.position.y);

        int East = EastMap.GetTileData(EastMap.Width / 2, 0).X;
        int West = WestMap.GetTileData(WestMap.Width / 2, 0).X;
        int North = NorthMap.GetTileData(0, NorthMap.Height / 2).Y;
        int South = SouthMap.GetTileData(0, SouthMap.Height / 2).Y;

        if (time <= Time.time)
        {
            if (playerX > East)
            {
                //Time.timeScale = 0;
                Thread thread = new Thread(StepRight);
                thread.Start();
                //StepRight();

                //ClearColliders();
                StartCoroutine(WaitForThreadInstantiateRelocate(thread, direction.left));
                Time.timeScale = 1;
                time = Time.time + loadingTimer;

            }
            else if (playerX < West)
            {

                //Time.timeScale = 0;
                Thread thread = new Thread(StepLeft);
                thread.Start();
                //StepLeft();


                //ClearColliders();

                StartCoroutine(WaitForThreadInstantiateRelocate(thread, direction.right));


                Time.timeScale = 1;
                time = Time.time + loadingTimer;
            }

            if (playerY > North)
            {
                
                //Time.timeScale = 0;
                Thread thread = new Thread(StepUp);
                thread.Start();
                //StepUp();


                //ClearColliders();
                StartCoroutine(WaitForThreadInstantiateRelocate(thread, direction.down));
                
                Time.timeScale = 1;
                time = Time.time + loadingTimer;
        
            }
            else if (playerY < South)
            {
                
                //Time.timeScale = 0;
                Thread thread = new Thread(StepDown);
                thread.Start();
                //StepDown();



                //ClearColliders();
                StartCoroutine(WaitForThreadInstantiateRelocate(thread,direction.up));
                

                //PreInstantiateAll();
                Time.timeScale = 1;
                time = Time.time + loadingTimer;


            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            StepUp();


            //Copy(WestMap.map, newMap(WestMap));
            //Copy(EastMap.map, newMap(EastMap));
            //Copy(NorthMap.map, newMap(NorthMap));
            //Copy(SouthMap.map, newMap(SouthMap));
            //Copy(CenterMap.map, newHub(CenterMap));

                

        }

        
            
            
    }

    direction transitionDir;
    bool transition = false;
    IEnumerator WaitForThreadInstantiateRelocate(Thread thread,direction relocationDir)
    {
        transitionDir = relocationDir;
        while (!transition)
        {
            
            yield return new WaitForSeconds(.01f);
        }

   
        //Camera.main.transform.Relocate(relocationDir, WestMap.Width, NorthMap.Height, WestMap.Height);
        //RelocateAll(relocationDir);
        //PreInstantiateAll();
        

        yield return null;
        
    }




    private bool mapGenComplete = false;
    public void StepUp()
    {
        //Time.timeScale = 0;


        Copy(WestMap.map, newMap(WestMap));
        Copy(EastMap.map, newMap(EastMap));

        //Copy(SouthMap.map, newMap(SouthMap));



        //TODO: Clone, statement needs to be the last statement
        CopyWithStartPoints(SouthMap, NorthMap);
        transition = true;

        //center map should probably be run on a seperate thread as well
        Copy(NorthMap.map, newMap(NorthMap));
        
        Copy(CenterMap.map, newHub(CenterMap));

        mapGenComplete = true;

    }

    public void StepDown()
    {



        //Time.timeScale = 0;

        Copy(WestMap.map, newMap(WestMap));
        Copy(EastMap.map, newMap(EastMap));


        CopyWithStartPoints(NorthMap, SouthMap);


        transition = true;



        Copy(SouthMap.map, newMap(SouthMap));

        Copy(CenterMap.map, newHub(CenterMap));

        mapGenComplete = true;



    }

    public void StepLeft()
    {
        //Time.timeScale = 0;
        Debug.Log("stepping left");

        Copy(NorthMap.map, newMap(NorthMap));
        Copy(SouthMap.map, newMap(SouthMap));

        CopyWithStartPoints(EastMap, WestMap);
        transition = true;

        Copy(WestMap.map, newMap(WestMap));
        Copy(CenterMap.map, newHub(CenterMap));
        mapGenComplete = true;

    }

    public void StepRight()
    {
        //Time.timeScale = 0;


        Copy(NorthMap.map, newMap(NorthMap));
        Copy(SouthMap.map, newMap(SouthMap));



        
        //Copy(EastMap.map, newMap(EastMap));

        CopyWithStartPoints(WestMap, EastMap);
        transition = true;

        Copy(EastMap.map, newMap(EastMap));
        Copy(CenterMap.map, newHub(CenterMap));

        mapGenComplete = true;
                
    //
    }

    private TileStruct[][] clone(TileStruct[][] map)
    {
        var newMap = new TileStruct[map.Length][];
        for (int y = 0; y < map.Length; y++)
        {
            newMap[y] = new TileStruct[map[0].Length];
            for (int x = 0; x < map[0].Length; x++)
            {
                var currentTile = map[y][x];
                newMap[y][x] = new TileStruct(currentTile.X, currentTile.Y, currentTile.Type, currentTile.terrainType, currentTile.DecorType);
            }
        }
        return newMap;
    }

    

    public void RelocateAll(direction direction)
    {
        player.transform.Relocate(direction, WestMap.Width, NorthMap.Height, WestMap.Height);
        Camera.main.transform.Relocate(direction, WestMap.Width, NorthMap.Height, WestMap.Height);

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        var projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (var enemy in enemies)
        {
            enemy.transform.Relocate(direction, WestMap.Width, NorthMap.Height, WestMap.Height);
        }
        foreach (var projectile in projectiles)
        {
            projectile.transform.Relocate(direction, WestMap.Width, NorthMap.Height, WestMap.Height);
        }
        

    }


    public TileStruct[][] newMap(TileMap map)
    {
        var gen = new MapHandler(map.Width, map.Height, 48, TileStruct.getRandomTerrainType(stringToSeed(Hashtag + map.Hashtag)), stringToSeed(Hashtag + map.Hashtag));
        return gen.createMap(ref map.StartPointX, ref map.StartPointY, ref map.EndPointX, ref map.EndPointY);
    }

    public TileStruct[][] newHub(TileMap centerMap)
    {

        var gen = new MapHandler(centerMap.Width, centerMap.Height, 48, TileStruct.getRandomTerrainType(stringToSeed(Hashtag)), stringToSeed(Hashtag));
        return gen.createHub(WestMap, EastMap, SouthMap, NorthMap);
    
    }

    public int stringsToSeed(string first, string second)
    {
        return (first + second).GetHashCode();
         
    }
    public int stringToSeed(string first)
    {
        return first.GetHashCode();

    }

    

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


    public void CopyWithStartPoints(TileMap target, TileMap template)
    {
        Copy(target.map, template.map);
        target.StartPointX = template.StartPointX;
        target.StartPointY = template.StartPointY;
        target.EndPointX = template.EndPointX;
        target.EndPointY = template.EndPointY;
    
    }

    public void CopyWithStartPoints(TileMap target, TileStruct[][] template, int startX, int startY, int endX, int endY)
    {
        Copy(target.map, template);
        target.StartPointX = startX;
        target.StartPointY = startY;
        target.EndPointX = endX;
        target.EndPointY = endY;

    }





    public void ClearColliders()
    {

        Destroy(colliderContainer);
        colliderContainer = (GameObject)Instantiate(Resources.Load<GameObject>("ColliderContainer"));

    }


    public void PreInstantiateAll()
    {
        NorthMap.PreInstantiateColliders();
        SouthMap.PreInstantiateColliders();
        CenterMap.PreInstantiateColliders();
        WestMap.PreInstantiateColliders();
        EastMap.PreInstantiateColliders();
    }


    public void SwapVertical()
    { 
    
    }

    public void GenerateHub()
    {

    
    }



}
