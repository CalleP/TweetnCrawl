using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


/// <summary>
/// Class TileMap.
/// </summary>
public class TileMap : MonoBehaviour {

    public string Hashtag = "granat";

    public int Height = 20;
    public int Width  = 20;
    System.Random rand = new System.Random();

    public TileStruct[][] map;

    public int StartPointX;
    public int StartPointY;

    public int EndPointX;
    public int EndPointY;


    private List<GameObject> colliders = new List<GameObject>();


    /// <summary>
    /// Work around constructor DO NOT USE
    /// </summary>


	public virtual void Start () {




        map = new TileStruct[Height][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new TileStruct[Width];
        }


        for (int i = 0; i < 1000; i++)
        {
            GameObject coll = (GameObject)Instantiate(collider, new Vector3(0, 0, 0), Quaternion.identity);
            coll.transform.parent = colliderContainer.transform;
            colliders.Add(coll);
        }


    }
	
	void Update () {


	}

    /// <summary>
    /// Clear the map and create initialize a new one
    /// </summary>
    /// <param name="seed">The seed.</param>
    public void Restart(int seed) {
        Clear();
        var rand = new System.Random(seed);
        new MapGen(seed, Height, Width);
    }

    /// <summary>
    /// Modify the index of the map at specified coordinates
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="type">Tile type</param>
    public void SetTileStruct(int x, int y, TileType type)
    {
        Array values = Enum.GetValues(typeof(TileType));
        TileType randomBar = (TileType)values.GetValue(rand.Next(values.Length-1));
        map[x][y] = new TileStruct(x, y, randomBar);
    }

    /// <summary>
    /// Floods the map.
    /// </summary>
    /// <param name="type">The Tiletype you want it to be flooded with</param>
    public void FloodMap(TileType type)
    {
        int count = 0;
        for (int i = 0; i < Height; i++)
        {
            for (int y = 0; y < Width; y++)
            {
                SetTileStruct(i, y, type);
                count++;
            }
        }
    }

    public static TileStruct[][] CropMap(TileStruct[][] map,int x1, int y1, int x2, int y2)
    {
        int height = Mathf.Abs(y1 - y2);
        IEnumerable<TileStruct> arr = map.Skip(x1)
                            .Take(x2 - x1)
                            .SelectMany(a => a.Skip(y1).Take(y2 - y1));

         var array = arr.Select((x, i) => new { Index = i, Value = x })
        .GroupBy(x => x.Index / height)
        .Select(x => x.Select(v => v.Value).ToList().ToArray())
        .ToArray();

        TileStruct[][] outArray = new TileStruct[array.Length][];
         for (int i = 0; i < array.Length; i++)
		{
             outArray[i] = new TileStruct[array[i].Length];
            for (int y = 0; y < array[i].Length; y++)
            {
                var input = array[i][y];
                outArray[i][y] = new TileStruct(input.X, input.Y, input.Type);
            }
		}

         return outArray;
    }

    //no cloning
    public static TileStruct[][] CropMap2(TileStruct[][] map, int x1, int y1, int x2, int y2, TileType corridor)
    {
        int height = Mathf.Abs(y1 - y2);
        IEnumerable<TileStruct> arr = map.Skip(x1)
                            .Take(x2 - x1)
                            .SelectMany(a => a.Skip(y1).Take(y2 - y1));

        var array = arr.Select((x, i) => new { Index = i, Value = x })
       .GroupBy(x => x.Index / height)
       
       .Select(x => x.Select(v => v.Value).ToList().ToArray())
   
       .ToArray();


        foreach (var item in array)
        {
            foreach (var item2 in item)
            {
                item2.Type = corridor;
            }
        }

        

        return array;
    }


    /// <summary>
    /// Gets the tile data at specified map coordinates
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>TileStruct.</returns>
    public TileStruct GetTileData(int x, int y)
    {
        var outType = new TileStruct(x, y, TileType.None);
        if (x < 0 || x >= map[0].Length)
        {
            outType.Type = TileType.None;
            
        }
        else if (y < 0 || y >= map.Length)
        {
            outType.Type = TileType.None;
        }
        else
        {

                return map[y][x];


        }
        return outType;

    }


    public TileStruct GetTileData2(int x, int y)
    {
        var outType = new TileStruct(x, y, TileType.None);
        if (x < 0 || x >= map[0].Length)
        {
            outType.Type = TileType.None;

        }
        else if (y < 0 || y >= map.Length)
        {
            outType.Type = TileType.None;
        }
        else
        {
            var oldTile = map[y][x];
            oldTile.X = x;
            oldTile.Y = y;
            return oldTile;


        }
        return outType;

    }


    //Remove this?
    public TileStruct GetTileDataCoords(float x, float y)
    {
        x = x / 3.2f;
        y = y / 3.2f;

        int outX = 0;
        int outY = 0;
        if (x <= 0)
        {
            outX = 0;
        }
        else if (x >= Width)
        {
            outX = Width;
        }
        else
        {
            outX = (int)x;
        }

        if (y <= 0)
        {
            outY = 0;
        }
        else if (y >= Height)
        {
            outY = Height;
        }
        else 
        {
            outY = (int)y;
        }

        return map[outX][outY];
        
    }


    /// <summary>
    /// Gets the tile at the specified map coordinates
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>Tile.</returns>
    public Tile GetTile(int x, int y)
    {
        Collider2D[] colliders;
        if ((colliders = Physics2D.OverlapPointAll(new Vector2(x,y))).Length > 1) //Presuming the object you are testing also has a collider 0 otherwise
        {
            foreach (var collider in colliders)
            {
                if (collider.gameObject.tag == "Tile")
                {
                    return collider.gameObject.GetComponent<Tile>();
                    
                }
            }
        }
        return null;
    }


    /// <summary>
    /// Clears the map
    /// </summary>
    private void Clear() { 
        //TODO: Complete function
    }


    /// <summary>
    /// Draws the map.
    /// </summary>
    /// <param name="map">The map.</param>
    public void DrawMap(TileStruct[][] map)
    {
        var prefab = Resources.Load("Tile");
        foreach (var item in map)
        {
            foreach (var item2 in item)
            {
                var tile = (GameObject)Instantiate(prefab, new Vector3(((float)item2.X*3.2f),((float)item2.Y*3.2f)), Quaternion.identity);
                tile.transform.parent = gameObject.transform;
                var script = (Tile)tile.GetComponent<Tile>();
                script.TileData = item2;
            }
        }
    }


    //Remove?
    public void DrawMap2()
    {
        var prefab = Resources.Load("Tile");
        foreach (var item in map)
        {
            foreach (var item2 in item)
            {
                var tile = (GameObject)Instantiate(prefab, new Vector3(((float)item2.X * 3.2f), ((float)item2.Y * 3.2f)), Quaternion.identity);
                tile.transform.parent = gameObject.transform;
                var script = (Tile)tile.GetComponent<Tile>();
                script.TileData = item2;
            }
        }
    }


    



    public void PlaceBorders(TileType type)
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                if (y == 0 || y == map.Length-1)
	            {
                    map[y][x].Type = type;
	            }
                else if (x == 0 || x == map[0].Length-1)
                {
                    map[y][x].Type = type;
                }

            }
        }
    }


    public TileStruct ClosestToBorderX(TileType type)
    {
        int bestValue = map[0].Length;
        TileStruct bestMatch = null;
        for (int y = 0; y < map.Length; y++)
		{
			 for (int x = 0; x < map[0].Length; x++)
			{

			    if (map[y][x].Type == type && x < bestValue)
                {
                    bestValue = x;
                    bestMatch = map[y][x];
                }
			}
		}

        
        return bestMatch;
    }


    public TileStruct ClosestToBorderXReverse(TileType type)
    {
        int bestValue = 0;
        TileStruct bestMatch = null;
        for (int y = map.Length-1; y >= 0; y--)
		{
			for (int x = map[0].Length-1; x >= 0; x--)
			{
                if (map[y][x].Type == type && x > bestValue)
                {
                    bestValue = map[y][x].X;
                    bestMatch = map[y][x];
                }
            }
        }
        return bestMatch;
    }



    public TileStruct ClosestToBorderY(TileType type)
    {
        int bestValue = map.Length;
        TileStruct bestMatch = null;
        foreach (var y in map)
        {
            foreach (var x in y)
            {
                if (x.Type == type && x.Y < bestValue)
                {
                    bestValue = x.Y;
                    bestMatch = x;
                }
            }
        }
        return bestMatch;
    }

    public TileStruct ClosestToBorderYReverse(TileType type)
    {
        int bestValue = 0;
        TileStruct bestMatch = null;
        for (int y = map.Length - 1; y >= 0; y--)
        {
            for (int x = map[0].Length - 1; x >= 0; x--)
            {
                if (map[y][x].Type == type && map[y][x].Y > bestValue)
                {
                    bestValue = map[y][x].Y;
                    bestMatch = map[y][x];
                }
            }
        }
        return bestMatch;
    }


    public void SpawnStartExitPoint(TileStruct location, bool exit)
    { 
        if (!exit)
	    {
            int count = -1;
            TileStruct x = GetTileData(location.X, location.Y);

            int distance = 0 - location.X;

            for (int i = distance; i <= 0; i++)
            {
                x = GetTileData(location.X+i, location.Y);
                x.SetBoth(TerrainType.BlackCave);
                x.Type = TileType.Dirt;

                var downWalls = GetTileData(location.X + i, location.Y-1);
                downWalls.SetBoth(TerrainType.BlackCave);
                downWalls.Type = TileType.Rock;
                var upWalls = GetTileData(location.X + i, location.Y+1);
                upWalls.SetBoth(TerrainType.BlackCave);
                upWalls.Type = TileType.Rock;


                if (i == 0)
                {
                    var Wall = GetTileData(location.X + i + 1, location.Y);
                    Wall.SetBoth(TerrainType.BlackCave);
                    Wall.Type = TileType.Dirt;

                    Wall = GetTileData(location.X + i +1, location.Y-1);
                    Wall.SetBoth(TerrainType.BlackCave);
                    Wall.Type = TileType.Dirt;

                    Wall = GetTileData(location.X + i + 1, location.Y + 1);
                    Wall.SetBoth(TerrainType.BlackCave);
                    Wall.Type = TileType.Dirt;

                    Wall = GetTileData(location.X + i + 2, location.Y);
                    Wall.SetBoth(TerrainType.BlackCave);
                    Wall.Type = TileType.Dirt;

                    //Wall = GetTileData(location.X + i + 1, location.Y +1);
                    //Wall.SetBoth(TerrainType.BlackCaste);
                    //Wall.Type = TileType.Dirt;

                    //Wall = GetTileData(location.X + i + 1, location.Y - 3);
                    //Wall.SetBoth(TerrainType.BlackCaste);
                    //Wall.Type = TileType.Dirt;

                    //Wall = GetTileData(location.X + i + 2, location.Y - 3);
                    //Wall.SetBoth(TerrainType.BlackCaste);
                    //Wall.Type = TileType.Dirt;
                }
            }
	    }
        
    }

    public void DrawCorridorHorizontal(int x1, int x2, int y, TileType wallType, TileType floorType, TerrainType wallTerrainType, TerrainType floorTerrainType)
    {
        int length = Mathf.Abs(x1 - x2)+1;

        int distanceA = x1 - x2;
        int distanceB = x1 - x2;


        for (int i = 0; i < length; i++)
        {
            TileStruct pointA;
            if (distanceA <= 0)
            {
              pointA = GetTileData(x1 + i, y);
            }
            else
            {
                pointA = GetTileData(x1 - i, y);
            }
            pointA.Type = floorType;
            pointA.SetFloor(floorTerrainType);



        }


    }


    public void DrawCorridorHVertical(int y1, int y2, int x, TileType wallType, TileType floorType, TerrainType wallTerrainType, TerrainType floorTerrainType)
    {
        int length = Mathf.Abs(y1 - y2) + 1;

        int distanceA = y1 - y2;
        int distanceB = y1 - y2;


        for (int i = 0; i < length; i++)
        {
            TileStruct pointA;
            if (distanceA <= 0)
            {
                pointA = GetTileData(x, y1 + i);
            }
            else
            {
                pointA = GetTileData(x, y1 - i);
            }
            pointA.Type = floorType;
            pointA.SetFloor(floorTerrainType);



        }


    }

    
    public int TilesBeside(int x, int y, direction direction, TileType type)
    {
        int count = 0;
        if (direction == direction.up)
        {
            y++;
            while (GetTileData(x,y).Type == type)
            {
                count++;
                y++;
            }
            return count;
            
        }
        else if (direction == direction.down)
        {
            y--;
            while (GetTileData(x, y).Type == type)
            {
                count++;
                y--;
            }
            return count;
        }
        else if (direction == direction.left)
        {
            x--;
            while (GetTileData(x, y).Type == type)
            {
                count++;
                x--;
            }
            return count;
        }
        else
        {
            x++;
            while (GetTileData(x, y).Type == type)
            {
                count++;
                x++;
            }
            return count;
        }
    }

    public int count = 0;
    public void TrimMap()
    {
        count = 0;
        foreach (var y2 in map)
        {
   
            foreach (var x2 in y2)
            {
                int surroundingTiles = 0;
                var currentTile = x2;
                var x = currentTile.X;
                var y = currentTile.Y;
                
                if (GetTileData(x - 1, y).Type == TileType.Dirt) { surroundingTiles++; };

                if (GetTileData(x + 1, y).Type == TileType.Dirt) { surroundingTiles++; }
                if (GetTileData(x, y + 1).Type == TileType.Dirt) { surroundingTiles++; }
                if (GetTileData(x, y - 1).Type == TileType.Dirt) { surroundingTiles++; }

                if (surroundingTiles == 3 && currentTile.Type == TileType.Rock)
                {
                    currentTile.Type = TileType.Dirt;

                } 

            }
        }


    }

    public void Copy(TileStruct[][] original, TileStruct[][] newCopy)
    {
        for (int y = 0; y < original.Length; y++)
        {
            for (int x = 0; x < original[0].Length; x++)
            {
                var tile = newCopy[y][x];
                original[y][x].Type = tile.Type;
                original[y][x].DecorType = tile.DecorType;
                original[y][x].SetBoth(tile.terrainType);


            }
        }


    }



    public GameObject collider;

    private int previousColliderArraySize = 0;

    public GameObject colliderContainer;
    public void PreInstantiateColliders()
    {


        var vector3 = new Vector3(0, 0, 0);


        int count = 0;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {

                TileStruct currentTile = GetTileData(x, y);
                if (currentTile.Type == TileType.Rock)
                {


                    int surroundingTiles = 0;

                    var x2 = x;
                    var y2 = y;




                    if (GetTileData(x2 - 1, y2).Type == TileType.Rock) { surroundingTiles++; };

                    if (GetTileData(x2 + 1, y2).Type == TileType.Rock) { surroundingTiles++; }
                    if (GetTileData(x2, y2 + 1).Type == TileType.Rock) { surroundingTiles++; }
                    if (GetTileData(x2, y2 - 1).Type == TileType.Rock) { surroundingTiles++; }


                    if (surroundingTiles != 4)
                    {


                        //time3 = Time.realtimeSinceStartup;

                        vector3.x = currentTile.X * 3.2f;
                        vector3.y = currentTile.Y * 3.2f;
                        try
                        {
                            colliders[count].transform.position = vector3;
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                        

                        //btime3 = Time.realtimeSinceStartup;


                        count++;

                    }

                }

            }

        }


        int unchangedValues = previousColliderArraySize - count;

        var vector = new Vector3(0, 0, 0);
        for (int i = 0; i < unchangedValues; i++)
        {
            colliders[count + i].transform.position = vector;
        }



        previousColliderArraySize = count;

        
    }

    public int BasicsAmount = 5;
    public int SpecialistAmount = 4;
    public int ElitesAmount = 3;
    public int BossAmount = 1;

    public List<GameObject> monsters = new List<GameObject>();

    public bool ReadyToPopulate = true;

    public void PopulateMap(bool eastOrNorth)
    {
        var currentTT = map[0][0].terrainType; 

        for (int i = 0; i < BasicsAmount; i++)
        {
            var tile = findAvailableTile(eastOrNorth);
            var obj = (GameObject)Instantiate(Resources.Load("BasicEnemy"), new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -0.15f), Quaternion.identity);
            obj.GetComponent<EnemyRandomizer>().RandomizeFrames(EnemyTypes.Basic, currentTT);
            obj.GetComponent<BaseEnemy>().terrainType = currentTT;
            monsters.Add(obj);
        }

        for (int i = 0; i < SpecialistAmount; i++)
        {
            var tile = findAvailableTile(eastOrNorth);
            var obj = (GameObject)Instantiate(Resources.Load("TeleporterEnemy"), new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -0.15f), Quaternion.identity);
            obj.GetComponent<EnemyRandomizer>().RandomizeFrames(EnemyTypes.Teleporter, currentTT);
            obj.GetComponent<BaseEnemy>().terrainType = currentTT;
            monsters.Add(obj);
        }

        for (int i = 0; i < ElitesAmount; i++)
        {
            var tile = findAvailableTile(eastOrNorth);
            var obj = (GameObject)Instantiate(Resources.Load("HiveEnemy"), new Vector3(tile.X * 3.2f, tile.Y * 3.2f, -0.15f), Quaternion.identity);
            obj.GetComponent<EnemyRandomizer>().RandomizeFrames(EnemyTypes.Hive, currentTT);
            obj.GetComponent<BaseEnemy>().terrainType = currentTT;
            monsters.Add(obj);
        }


    
    }
    public void ClearEnemies() 
    {
        foreach (var item in monsters)
        {
            if (item != null)
            {
                Destroy(item);
            }

        }
        monsters = new List<GameObject>();
    }

    public void SetActiveOnEnemies(bool active)
    {
        foreach (var item in monsters)
        {
            if (item != null)
            {
                item.SetActive(active);
            }

        }
    }


    private TileStruct findAvailableTile(bool EastOrNorth)
    {
        TileStruct tile = new TileStruct(0, 0, TileType.None);
        while (tile.Type != TileType.Dirt)
        {
            if (!EastOrNorth)
            {
                tile = GetTileData(rand.Next(0, (map[0].Length - 1) - 20), rand.Next(0, (map.Length - 1) - 20));
            }
            else
            {
                tile = GetTileData(rand.Next(20, (map[0].Length - 1)), rand.Next(20, (map.Length - 1)));
            }

        }

        return tile;
    }



}
