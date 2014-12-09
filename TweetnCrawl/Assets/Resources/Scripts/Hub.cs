using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime;

public class Hub :TileMap {

    public GameObject CurrentHashtagGUI;

    //public GameObject connector;

    public string[] friends;

    public bool test = false;

    public TileMap NorthMap;
    public TileMap WestMap;
    public TileMap SouthMap;
    public TileMap EastMap;
    public TileMap CenterMap;

    //Delete
    public string CenterMapHubHashtag;

    public string NorthHubHashtag;
    public string WestHubHashtag;
    public string SouthHubHashtag;
    public string EastHubHashtag;

    

    public TileStruct[][] FullMap;

    public bool WestToNorth;  
    public bool SouthToWest;
    public bool EastToSouth;

    public bool started = false;

    private int originalWidth;
    private int originalLength;


    private float loadingTimer = 5f;

    public bool MainHub;


    private string[] arrOfHashTags;

    public List<string> previouslyVisitedHubs = new List<string>();
    private bool readyToAddPoint = false;
    public int Points;


	public override void Start () {

        if (!MainHub)
        {
            base.Start();
        }


        var connect = new ServerConnector();

        connect.Connect();

            Debug.Log("Failed to connect starting offlineMode");


        arrOfHashTags = connect.ParseHashtag(connect.Send("Test"));

        connect.Close();

        Hashtag = arrOfHashTags[Random.Range(0, arrOfHashTags.Length - 1)];



        CenterMap.Hashtag = arrOfHashTags[Random.Range(0, arrOfHashTags.Length - 1)];
        EastHubHashtag = arrOfHashTags[Random.Range(0, arrOfHashTags.Length - 1)];
        WestHubHashtag = arrOfHashTags[Random.Range(0, arrOfHashTags.Length - 1)];
        NorthHubHashtag = arrOfHashTags[Random.Range(0, arrOfHashTags.Length - 1)];
        SouthHubHashtag = arrOfHashTags[Random.Range(0, arrOfHashTags.Length - 1)];

        SouthMap.Hashtag = CenterMap.Hashtag + " - " + SouthHubHashtag;
        WestMap.Hashtag = CenterMap.Hashtag + " - " + WestHubHashtag;
        NorthMap.Hashtag = CenterMap.Hashtag + " - " + NorthHubHashtag;
        EastMap.Hashtag = CenterMap.Hashtag + " - " + EastHubHashtag;




        SouthMap.map = newMap(SouthMap);
        NorthMap.map = newMap(NorthMap);


        EastMap.map = newMap(EastMap);

        WestMap.map = newMap(WestMap);

        CenterMap.map = newHub(CenterMap);

        

        CurrentHashtagGUI.GetComponent<GUIText>().text = CenterMap.Hashtag;



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

        PlaceAllRoadSigns();



        MergeAll();
        started = true;


        //DrawMap(arr);



        if (MainHub)
        {
            PreInstantiateAll();
        }

        

        ObjectPlacer.testStart();
        previouslyVisitedHubs.Add(CenterMap.Hashtag);

        //NorthMap.PopulateMap(true);
        //WestMap.PopulateMap(false);
        //EastMap.PopulateMap(true);
        //SouthMap.PopulateMap(false);

	}
	
	// Update is called once per frame

    private float time;
    public GameObject player;
    
    void Update () 
    {
        CenterMapHubHashtag = CenterMap.Hashtag;
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
            var signs = GameObject.FindGameObjectsWithTag("Sign");
            foreach (var item in signs)
            {
                Destroy(item);
            }
            PlaceAllRoadSigns();

            PreInstantiateAll();

            mapGenComplete = false;




        }

        var playerX = TileStruct.UnityUnitToTileUnit(player.transform.position.x);
        var playerY = TileStruct.UnityUnitToTileUnit(player.transform.position.y);

        int East = EastMap.GetTileData(EastMap.Width / 2, 0).X;
        int West = WestMap.GetTileData(WestMap.Width / 2, 0).X;
        int North = NorthMap.GetTileData(0, NorthMap.Height / 2).Y;
        int South = SouthMap.GetTileData(0, SouthMap.Height / 2).Y;

        //EastMap.SetActiveOnEnemies(true);

        //if (playerX > EastMap.map[0][0].X + 5)
        //{
        //    CurrentHashtagGUI.GetComponent<GUIText>().text = EastMap.Hashtag;

        //}
        //else if (playerX < WestMap.map[0][WestMap.Width - 1].X - 5)
        //{
        //    CurrentHashtagGUI.GetComponent<GUIText>().text = WestMap.Hashtag;
        //}

        //if (playerY > NorthMap.map[0][0].Y + 5)
        //{


        //}
        //else if (playerY < SouthMap.map[SouthMap.Height - 1][0].Y - 5)
        //{
        //    CurrentHashtagGUI.GetComponent<GUIText>().text = SouthMap.Hashtag;

        //}


        if (playerX > EastMap.map[0][0].X + 5 && EastMap.ReadyToPopulate)
        {
            EastMap.PopulateMap(true);
            EastMap.ReadyToPopulate = false;

            WestMap.ReadyToPopulate = true;
            WestMap.ClearEnemies();
            NorthMap.ReadyToPopulate = true;
            NorthMap.ClearEnemies();
            SouthMap.ReadyToPopulate = true;
            SouthMap.ClearEnemies();

            CurrentHashtagGUI.GetComponent<GUIText>().text = CenterMap.Hashtag + " - " + EastHubHashtag;
            
        }
        else if (playerX < WestMap.map[0][WestMap.Width - 1].X - 5 && WestMap.ReadyToPopulate)
        {
            WestMap.PopulateMap(false);
            WestMap.ReadyToPopulate = false;

            EastMap.ReadyToPopulate = true;
            EastMap.ClearEnemies();

            NorthMap.ReadyToPopulate = true;
            NorthMap.ClearEnemies();

            SouthMap.ReadyToPopulate = true;
            SouthMap.ClearEnemies();

            CurrentHashtagGUI.GetComponent<GUIText>().text = CenterMap.Hashtag + " - " + WestHubHashtag;

        }

        if (playerY > NorthMap.map[0][0].Y + 5 && NorthMap.ReadyToPopulate)
        {
            NorthMap.PopulateMap(true);
            NorthMap.ReadyToPopulate = false;

            EastMap.ReadyToPopulate = true;
            EastMap.ClearEnemies();

            WestMap.ReadyToPopulate = true;
            WestMap.ClearEnemies();

            SouthMap.ReadyToPopulate = true;
            SouthMap.ClearEnemies();

            CurrentHashtagGUI.GetComponent<GUIText>().text = CenterMap.Hashtag + " - " + NorthHubHashtag;

        }
        else if (playerY < SouthMap.map[SouthMap.Height - 1][0].Y - 5 && SouthMap.ReadyToPopulate)
        {
            SouthMap.PopulateMap(false);
            SouthMap.ReadyToPopulate = false;

            EastMap.ReadyToPopulate = true;
            EastMap.ClearEnemies();

            WestMap.ReadyToPopulate = true;
            WestMap.ClearEnemies();

            NorthMap.ReadyToPopulate = true;
            NorthMap.ClearEnemies();

            CurrentHashtagGUI.GetComponent<GUIText>().text = CenterMap.Hashtag + " - " + SouthHubHashtag;

        }
        else if ((playerY < NorthMap.map[0][0].Y && playerY > SouthMap.map[SouthMap.Height-1][0].Y) && (playerX > WestMap.map[0][WestMap.Width-1].X && playerX < EastMap.map[0][0].X))
        {
            if (readyToAddPoint)
            {
                Points++;
                UpDifficulty();
                previouslyVisitedHubs.Add(CenterMap.Hashtag);
                readyToAddPoint = false;
            }

            SouthMap.ReadyToPopulate = true;
            EastMap.ReadyToPopulate = true;
            WestMap.ReadyToPopulate = true;
            NorthMap.ReadyToPopulate = true;

            DestroyAll();

            CurrentHashtagGUI.GetComponent<GUIText>().text = CenterMap.Hashtag;


        }


        if (time <= Time.time)
        {
            if (playerX > East)
            {
                


                //Time.timeScale = 0;
                WestMap.ReadyToPopulate = false;

                Thread thread = new Thread(StepRight);
                thread.Start();
                //StepRight();

                //ClearColliders();
                StartCoroutine(WaitForThreadInstantiateRelocate(thread, direction.left));


                WestMap.ClearEnemies();
                WestMap.monsters = EastMap.monsters;


                EastMap.monsters = new List<GameObject>();

                NorthMap.ClearEnemies();
                SouthMap.ClearEnemies();

                Time.timeScale = 1;
                time = Time.time + loadingTimer;

                Debug.Log("Has stepped right:" + rightCount);

            }
            else if (playerX < West)
            {


                EastMap.ReadyToPopulate = false;

                //Time.timeScale = 0;
                Thread thread = new Thread(StepLeft);
                thread.Start();
                //StepLeft();


                //ClearColliders();

                StartCoroutine(WaitForThreadInstantiateRelocate(thread, direction.right));


                EastMap.ClearEnemies();
                EastMap.monsters = NorthMap.monsters;


                WestMap.monsters = new List<GameObject>();

                NorthMap.ClearEnemies();
                SouthMap.ClearEnemies();


                Time.timeScale = 1;
                time = Time.time + loadingTimer;

                Debug.Log("Has stepped left:" + leftCount);

            }

            if (playerY > North)
            {


                SouthMap.ReadyToPopulate = false;
                //Time.timeScale = 0;
                Thread thread = new Thread(StepUp);
                thread.Start();
                //StepUp();


                //ClearColliders();
                StartCoroutine(WaitForThreadInstantiateRelocate(thread, direction.down));

                SouthMap.ClearEnemies();
                SouthMap.monsters = NorthMap.monsters;


                NorthMap.monsters = new List<GameObject>();

                WestMap.ClearEnemies();
                EastMap.ClearEnemies();


                
                Time.timeScale = 1;
                time = Time.time + loadingTimer;


            }
            else if (playerY < South)
            {



                NorthMap.ReadyToPopulate = false;

                //Time.timeScale = 0;
                Thread thread = new Thread(StepDown);
                thread.Start();
                //StepDown();



                //ClearColliders();
                StartCoroutine(WaitForThreadInstantiateRelocate(thread,direction.up));
                NorthMap.ClearEnemies();
                NorthMap.monsters = SouthMap.monsters;


                SouthMap.monsters = new List<GameObject>();

                WestMap.ClearEnemies();
                EastMap.ClearEnemies();


                
                Time.timeScale = 1;
                time = Time.time + loadingTimer;


            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PreInstantiateAll();


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
        arrOfHashTags = getHashtags();

        SouthHubHashtag = CenterMap.Hashtag;
        CenterMap.Hashtag = NorthHubHashtag;

        System.Random rand = new System.Random();

        EastHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];
        WestHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];
        NorthHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];

        SouthMap.Hashtag = CenterMap.Hashtag + " - " + SouthHubHashtag;
        WestMap.Hashtag = CenterMap.Hashtag + " - " + WestHubHashtag;
        NorthMap.Hashtag = CenterMap.Hashtag + " - " + NorthHubHashtag;
        EastMap.Hashtag = CenterMap.Hashtag + " - " + EastHubHashtag;



        Copy(WestMap.map, newMap(WestMap));
        Copy(EastMap.map, newMap(EastMap));

        //Copy(SouthMap.map, newMap(SouthMap));



        //TODO: Clone, statement needs to be the last statement
        CopyWithStartPoints(SouthMap, NorthMap);
        transition = true;

        //center map should probably be run on a seperate thread as well
        Copy(NorthMap.map, newMap(NorthMap));
        
        Copy(CenterMap.map, newHub(CenterMap));

        readyToAddPoint = true;
        mapGenComplete = true;

    }

    public void StepDown()
    {

        arrOfHashTags = getHashtags();

        NorthHubHashtag = CenterMap.Hashtag;
        CenterMap.Hashtag = SouthHubHashtag;

        System.Random rand = new System.Random();

        EastHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];
        WestHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];
        SouthHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];

        SouthMap.Hashtag = CenterMap.Hashtag + " - " + SouthHubHashtag;
        WestMap.Hashtag = CenterMap.Hashtag + " - " + WestHubHashtag;
        NorthMap.Hashtag = CenterMap.Hashtag + " - " + NorthHubHashtag;
        EastMap.Hashtag = CenterMap.Hashtag + " - " + EastHubHashtag;

        //Time.timeScale = 0;

        Copy(WestMap.map, newMap(WestMap));
        Copy(EastMap.map, newMap(EastMap));


        CopyWithStartPoints(NorthMap, SouthMap);


        transition = true;



        Copy(SouthMap.map, newMap(SouthMap));

        Copy(CenterMap.map, newHub(CenterMap));

        readyToAddPoint = true;
        mapGenComplete = true;



    }

    int leftCount = 0;
    public void StepLeft()
    {
        leftCount++;
       
        arrOfHashTags = getHashtags();



        EastHubHashtag = CenterMap.Hashtag;
        CenterMap.Hashtag = WestHubHashtag;

        System.Random rand = new System.Random();

        
        WestHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];
        SouthHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];
        NorthHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];

        SouthMap.Hashtag = CenterMap.Hashtag + " - " + SouthHubHashtag;
        WestMap.Hashtag = CenterMap.Hashtag + " - " + WestHubHashtag;
        NorthMap.Hashtag = CenterMap.Hashtag + " - " + NorthHubHashtag;
        EastMap.Hashtag = CenterMap.Hashtag + " - " + EastHubHashtag;


        //Time.timeScale = 0;

        Copy(NorthMap.map, newMap(NorthMap));
        Copy(SouthMap.map, newMap(SouthMap));

        CopyWithStartPoints(EastMap, WestMap);
        transition = true;

        Copy(WestMap.map, newMap(WestMap));
        Copy(CenterMap.map, newHub(CenterMap));

        readyToAddPoint = true;
        mapGenComplete = true;

    }

    public int rightCount = 0;
    public void StepRight()
    {
        rightCount++;
        //Time.timeScale = 0;


        arrOfHashTags = getHashtags();



        WestHubHashtag = CenterMap.Hashtag;
        CenterMap.Hashtag = EastHubHashtag;

        System.Random rand = new System.Random();


        EastHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];
        SouthHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];
        NorthHubHashtag = arrOfHashTags[rand.Next(0, arrOfHashTags.Length - 1)];

        SouthMap.Hashtag = CenterMap.Hashtag + " - " + SouthHubHashtag;
        WestMap.Hashtag = CenterMap.Hashtag + " - " + WestHubHashtag;
        NorthMap.Hashtag = CenterMap.Hashtag + " - " + NorthHubHashtag;
        EastMap.Hashtag = CenterMap.Hashtag + " - " + EastHubHashtag;




        Copy(NorthMap.map, newMap(NorthMap));
        Copy(SouthMap.map, newMap(SouthMap));



        
        //Copy(EastMap.map, newMap(EastMap));

        CopyWithStartPoints(WestMap, EastMap);
        transition = true;

        Copy(EastMap.map, newMap(EastMap));
        Copy(CenterMap.map, newHub(CenterMap));

        readyToAddPoint = true;

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

        var pickups = GameObject.FindGameObjectsWithTag("Pickup");

        var shells = GameObject.FindGameObjectsWithTag("Shell");

        foreach (var enemy in enemies)
        {
            enemy.transform.Relocate(direction, WestMap.Width, NorthMap.Height, WestMap.Height);
        }
        foreach (var projectile in projectiles)
        {
            projectile.transform.Relocate(direction, WestMap.Width, NorthMap.Height, WestMap.Height);
        }

        foreach (var pickup in  pickups)
        {
            pickup.transform.Relocate(direction, WestMap.Width, NorthMap.Height, WestMap.Height);
        }

        foreach (var shell in shells)
        {
            shell.transform.Relocate(direction, WestMap.Width, NorthMap.Height, WestMap.Height);
        }
        
    }

    public void DestroyAll()
    {

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");


        var pickups = GameObject.FindGameObjectsWithTag("Pickup");

        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        foreach (var pickup in pickups)
        {
            Destroy(pickup);
        }

    }



    public TileStruct[][] newMap(TileMap map)
    {
        var gen = new MapHandler(map.Width, map.Height, 48, TileStruct.getRandomTerrainType(stringToSeed(Hashtag + map.Hashtag)), stringToSeed(Hashtag + map.Hashtag));
        return gen.createMap(ref map.StartPointX, ref map.StartPointY, ref map.EndPointX, ref map.EndPointY);
    }

    public TileStruct[][] newHub(TileMap centerMap)
    {

        var gen = new MapHandler(centerMap.Width, centerMap.Height, 48, TileStruct.getRandomTerrainType(stringToSeed(centerMap.Hashtag)), stringToSeed(centerMap.Hashtag));
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

    

    public string[] getHashtags()
    {
        var connect = new ServerConnector();

        connect.Connect();


        arrOfHashTags = connect.ParseHashtag(connect.Send("Test"));

        connect.Close();

        return arrOfHashTags;
    }

     float native_width = 1920;
     float native_height = 1080;





     public void PlaceRoadsign(TileStruct[][] map, TileStruct start, string hash)
     {


         Queue<TileStruct> stack = new Queue<TileStruct>();
         
         foreach (var item in map)
         {
             foreach (var item2 in item)
             {
                 item2.Visited = false;
             }
         }
         stack.Enqueue(start);

         int sentinel = 0;
         while (stack.Count != 0 || sentinel > 400)
         {
             sentinel++;

             var tile = stack.Dequeue();



             tile.Debug = true;

             var west = MapHandler.GetTileData(map, tile.X - 1, tile.Y);
             var east = MapHandler.GetTileData(map, tile.X + 1, tile.Y);
             var north = MapHandler.GetTileData(map, tile.X, tile.Y + 1);
             var south = MapHandler.GetTileData(map, tile.X, tile.Y - 1);

             if (tile.X >= WestMap.Width-1)
             {
                 west = MapHandler.GetTileData(map, (tile.X - WestMap.Width) - 1, (tile.Y - SouthMap.Height));
                 east = MapHandler.GetTileData(map, (tile.X - WestMap.Width) + 1, (tile.Y - SouthMap.Height));
                 north = MapHandler.GetTileData(map, (tile.X - WestMap.Width), (tile.Y - SouthMap.Height) + 1);
                 south = MapHandler.GetTileData(map, (tile.X - WestMap.Width), (tile.Y - SouthMap.Height) - 1);
             }


             List<TileStruct> dirts = new List<TileStruct>();
             if (west.Type  == TileType.Dirt)   dirts.Add(west);
             if (east.Type  == TileType.Dirt)   dirts.Add(east); 
             if (north.Type == TileType.Dirt)   dirts.Add(north); 
             if (south.Type == TileType.Dirt)   dirts.Add(south); 

             if (dirts.Count >= 3)
             {
                 foreach (var dirt in dirts)
                 {
                     if (tile.X != start.X && tile.Y != start.Y)
                     {
                        if(tile.X >= WestMap.Width-1)
                        {
                            var obj = (GameObject)Instantiate(Resources.Load("RoadSign"), new Vector3((tile.X) * 3.2f, (tile.Y) * 3.2f, -0.5f), Quaternion.identity);
                            obj.transform.GetChild(0).GetComponent<RoadSignBehaviour>().text = hash;
                            return;
                        }
                        else
	                    {
                            var obj = (GameObject)Instantiate(Resources.Load("RoadSign"), new Vector3((tile.X + WestMap.Width) * 3.2f, (tile.Y + SouthMap.Height) * 3.2f, -0.5f), Quaternion.identity);
                            obj.transform.GetChild(0).GetComponent<RoadSignBehaviour>().text = hash;
                            return;
	                    }

                         

                     }
                 }
             }

             if (west.Visited == false && west.Type == TileType.Dirt)   stack.Enqueue(west);
             if (east.Visited == false && east.Type == TileType.Dirt)   stack.Enqueue(east);
             if (north.Visited == false && north.Type == TileType.Dirt) stack.Enqueue(north);
             if (south.Visited == false && south.Type == TileType.Dirt) stack.Enqueue(south);



         }


     }

     private void PlaceAllRoadSigns()
     {
         PlaceRoadsign(CenterMap.map, CenterMap.map[WestMap.EndPointY][0], WestHubHashtag);
         PlaceRoadsign(CenterMap.map, CenterMap.map[EastMap.StartPointY][CenterMap.Width - 1], EastHubHashtag);

         PlaceRoadsign(CenterMap.map, CenterMap.map[0][SouthMap.EndPointX], SouthHubHashtag);
         PlaceRoadsign(CenterMap.map, CenterMap.map[CenterMap.Width - 1][NorthMap.StartPointX], NorthHubHashtag);
     }




     public GUIStyle style;
     void OnGUI ()
     {
        if (Event.current.type.Equals(EventType.Repaint)) {


		
            //set up scaling
            var rx = Screen.width / native_width;
            var ry = Screen.height / native_height;
            GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 
 
            //now create your GUI normally, as if you were in your native resolution
            //The GUI.matrix will scale everything automatically.
 
            //example
            GUI.Label(new Rect(5,210,200,200),CurrentHashtagGUI.guiText.text, style);
            GUI.Label(new Rect(5, 260, 200, 200), Points.ToString(), style);
        } 
     }

     public void UpDifficulty()
     {
         ModifyEnemyspawn(WestMap);
         ModifyEnemyspawn(EastMap);
         ModifyEnemyspawn(NorthMap);
         ModifyEnemyspawn(SouthMap);
     }
     public void ModifyEnemyspawn(TileMap map)
     {
         map.BasicsAmount += 5;
         map.SpecialistAmount += 2;

         map.BasicsAmount = Mathf.Clamp(map.BasicsAmount, 0, 20);
         map.SpecialistAmount = Mathf.Clamp(map.SpecialistAmount, 0, 10);


     }

 
}
