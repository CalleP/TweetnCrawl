﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


/// <summary>
/// Class TileMap.
/// </summary>
public class TileMap : MonoBehaviour {
    
    public int Height = 20;
    public int Width  = 20;
    System.Random rand = new System.Random();

    public TileStruct[][] map;

	void Start () {
        map = new TileStruct[Height][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new TileStruct[Width];
        }
        
        FloodMap(TileType.Dirt);

        //BSTgen.GenMap(map, 5, 10, 2, 3, 0, true);

        //DrawMap(
        //     BSTgen.addRoom(map,5,10,0));




        //////var map2 = CropMap(map, 0, 0, map.Length / 2, map.Length);
        //////var map3 = CropMap(map, map.Length / 2, 0, map.Length, map.Length);


        //////var corridorSize = 3;

        //////BSTgen.addRoom(map2, 0, 5, 0, TileType.Rock, TileType.Dirt);
        //////BSTgen.addRoom(map3, 0, 5, 0, TileType.Rock, TileType.Dirt);




        //////var map4 = BSTgen.horizontalMerge(map2, map3);

        //////TileMap.CropMap2(map4, 10, 10, 20, 20, TileType.Dirt);

        //////map = map4;


        //map = map4;
        var test = new MapHandler();
        
        test.MakeCaverns();


        TileStruct[][] convertedArray;
        convertedArray = new TileStruct[test.Map.GetLength(0)][];
        for (int i = 0; i < convertedArray.Length; i++)
        {
            convertedArray[i] = new TileStruct[test.Map.GetLength(1)];
        }

        for (int i = 0; i < test.Map.GetLength(0); i++)
        {

            for (int y = 0; y < test.Map.GetLength(1); y++)
			{
                if (test.Map[i, y] == 0)
                {
                    convertedArray[i][y] = new TileStruct(i,y,TileType.Dirt);
                }
                else if (test.Map[i, y] == 1)
                {
                    convertedArray[i][y] = new TileStruct(i, y, TileType.Rock);
                }
                
			}
        }




        convertedArray[0][0].Type = TileType.Dirt;

        //foreach (var item in TrimWalls())
        //{

        //    convertedArray[item.X][item.Y].Type = item.Type;
        //} 

        map = convertedArray;


        //DrawMap(map);

        //VerticalTest
        //var map2 = CropMap(0, 0, map.Length, map.Length / 2, map.Length / 2);
        //map2[2][2].Type = TileType.Rock;


        //var map3 = CropMap(0, map.Length / 2, map.Length, map.Length, map.Length);

        //map3[2][2].Type = TileType.Rock;
        //var map4 = BSTgen.verticalMerge(BSTgen.addRoom(map3, 0, 5, 0), BSTgen.addRoom(map2, 0, 5, 0));
        //map4[2][2].Type = TileType.Rock;
        //map4[2][18].Type = TileType.Rock;
         //DrawMap(map4);
         //DrawMap(map3);

        
        
        //DrawMap(BSTgen.addRoom(
        //   CropMap(0, 0, map.Length / 2, map.Length, 5)
        //   , 0, 2, 1)
        //   );
    }
	
	void Update () {
        //new MapGen(5, Height, Width);
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

    /// <summary>
    /// Crops the map between the specified map coordinates
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="height">The height.</param>
    /// <returns>TileStruct[][].</returns>
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
            return map[x][y];
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
                    Debug.Log("GotTileStruct");
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


    public bool TrimWalls() 
    {

        var output = false;
        for (int i = 0; i < map.Length; i++)
        {
            for (int y = 0; y < map[i].Length; y++)
            {
                var currentTile = map[i][y];
                int surroundingTiles = 0;
                if (GetTileData(y-1, i).Type == TileType.Dirt) surroundingTiles++;
                if (GetTileData(y + 1, i).Type == TileType.Dirt) surroundingTiles++;
                if (GetTileData(y, i + 1).Type == TileType.Dirt) surroundingTiles++;
                if (GetTileData(y, i - 1).Type == TileType.Dirt) surroundingTiles++;

                
                currentTile.surroundingDirts = surroundingTiles;
                output = true;

                

            }
        }
        return output;
    }
}
