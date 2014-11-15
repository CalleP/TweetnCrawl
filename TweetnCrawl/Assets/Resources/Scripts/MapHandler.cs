using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//We followed the tutorial http://csharpcodewhisperer.blogspot.se/2013/07/Rouge-like-dungeon-generation.html for most of the class
public class MapHandler
{
    System.Random rand = new System.Random();

    public int[,] Map;

    public int MapWidth { get; set; }
    public int MapHeight { get; set; }
    public int PercentAreWalls { get; set; }

    public MapHandler(int mapWidth, int mapHeight, int wallPercentage)
    {
        MapWidth = mapWidth;
        MapHeight = mapHeight;
        PercentAreWalls = wallPercentage;

        RandomFillMap();
    }

    public void MakeCaverns()
    {
        // By initilizing column in the outter loop, its only created ONCE
        for (int column = 0, row = 0; row <= MapHeight - 1; row++)
        {
            for (column = 0; column <= MapWidth - 1; column++)
            {
                Map[column, row] = PlaceWallLogic(column, row);
            }
        }
    }

    public int PlaceWallLogic(int x, int y)
    {
        int numWalls = GetAdjacentWalls(x, y, 1, 1);


        if (Map[x, y] == 1)
        {
            if (numWalls >= 4)
            {
                return 1;
            }
            return 0;
        }
        else
        {
            if (numWalls >= 5)
            {
                return 1;
            }
        }
        return 0;
    }

    public int GetAdjacentWalls(int x, int y, int scopeX, int scopeY)
    {
        int startX = x - scopeX;
        int startY = y - scopeY;
        int endX = x + scopeX;
        int endY = y + scopeY;

        int iX = startX;
        int iY = startY;

        int wallCounter = 0;

        for (iY = startY; iY <= endY; iY++)
        {
            for (iX = startX; iX <= endX; iX++)
            {
                if (!(iX == x && iY == y))
                {
                    if (IsWall(iX, iY))
                    {
                        wallCounter += 1;
                    }
                }
            }
        }
        return wallCounter;
    }

    bool IsWall(int x, int y)
    {
        // Consider out-of-bound a wall
        if (IsOutOfBounds(x, y))
        {
            return true;
        }

        if (Map[x, y] == 1)
        {
            return true;
        }

        if (Map[x, y] == 0)
        {
            return false;
        }
        return false;
    }

    bool IsOutOfBounds(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return true;
        }
        else if (x > MapWidth - 1 || y > MapHeight - 1)
        {
            return true;
        }
        return false;
    }

    public void BlankMap()
    {
        for (int column = 0, row = 0; row < MapHeight; row++)
        {
            for (column = 0; column < MapWidth; column++)
            {
                Map[column, row] = 0;
            }
        }
    }

    public void RandomFillMap()
    {
        // New, empty map
        Map = new int[MapWidth, MapHeight];

        int mapMiddle = 0; // Temp variable
        for (int column = 0, row = 0; row < MapHeight; row++)
        {
            for (column = 0; column < MapWidth; column++)
            {
                // If coordinants lie on the edge of the map
                // (creates a border)
                if (column == 0)
                {
                    Map[column, row] = 1;
                }
                else if (row == 0)
                {
                    Map[column, row] = 1;
                }
                else if (column == MapWidth - 1)
                {
                    Map[column, row] = 1;
                }
                else if (row == MapHeight - 1)
                {
                    Map[column, row] = 1;
                }
                // Else, fill with a wall a random percent of the time
                else
                {
                    mapMiddle = (MapHeight / 2);

                    if (row == mapMiddle)
                    {
                        Map[column, row] = 0;
                    }
                    else
                    {
                        Map[column, row] = RandomPercent(PercentAreWalls);
                    }
                }
            }
        }
    }

    int RandomPercent(int percent)
    {
        if (percent >= rand.Next(1, 101))
        {
            return 1;
        }
        return 0;
    }

    public MapHandler(int mapWidth, int mapHeight, int[,] map, int percentWalls = 40)
    {
        this.MapWidth = mapWidth;
        this.MapHeight = mapHeight;
        this.PercentAreWalls = percentWalls;
        this.Map = new int[this.MapWidth, this.MapHeight];
        this.Map = map;
    }



    public TileStruct[][] createMap(ref int startX, ref int startY, ref int endX, ref int endY)
    {
        BlankMap();
        RandomFillMap();

        MakeCaverns();


        TileStruct[][] convertedArray = GetAsTileStructArr();

        PlaceBorders(TileType.Rock, convertedArray);


        TrimMap(convertedArray);
        TrimMap(convertedArray);
        TrimMap(convertedArray);

        DrawStartAndEndPoints(convertedArray, ref startX,ref startY, ref endX, ref endY);

        MapChecker checker = new MapChecker(this, convertedArray);

        if (checker.CheckMap(
            GetTileData(convertedArray, startX, startY),
            GetTileData(convertedArray, endX, endY),
            direction.right))
        {
            return checker.map;
        }
        else
        {
            
           // return checker.map;
            
            return createMap(ref startX, ref startY, ref endX, ref endY);
        }


        

        //MapChecker checker = new MapChecker(this, map);
        //if (checker.CheckMap(closest, closest2, direction.right))
        //{
        //    break;
        //}

    }


    public TileStruct[][] createHub(TileMap westMap, TileMap eastMap, TileMap southMap, TileMap northMap)
    {
        BlankMap();
        RandomFillMap();

        MakeCaverns();


        TileStruct[][] convertedArray = GetAsTileStructArr();

        PlaceBorders(TileType.Rock, convertedArray);


        TrimMap(convertedArray);
        TrimMap(convertedArray);
        TrimMap(convertedArray);
        
        DrawHubStartAndEndPoints(convertedArray, westMap, eastMap, southMap, northMap);

        if (EastToSouth && SouthToWest && WestToNorth)
        {
            return convertedArray;
        }
        else
        {
            return createHub(westMap, eastMap, southMap, northMap);
        }



    }



    public TileStruct[][] GetAsTileStructArr() 
    {
        TileStruct[][] convertedArray;
        convertedArray = new TileStruct[Map.GetLength(1)][];
        for (int i = 0; i < convertedArray.Length; i++)
        {
            convertedArray[i] = new TileStruct[Map.GetLength(0)];
        }

        for (int x = 0; x < Map.GetLength(0); x++)
        {

            for (int y = 0; y < Map.GetLength(1); y++)
            {
                if (Map[x, y] == 0)
                {
                    convertedArray[y][x] = new TileStruct(x, y, TileType.Dirt);
                }
                else if (Map[x, y] == 1)
                {
                    convertedArray[y][x] = new TileStruct(x, y, TileType.Rock);
                }

            }
        }
        return convertedArray;
    }


    bool EastToSouth;
    bool SouthToWest;
    bool WestToNorth;
    public void DrawHubStartAndEndPoints(TileStruct[][] map, TileMap westMap, TileMap eastMap, TileMap southMap, TileMap northMap)
    {

            var NorthPoint = map[map.Length - 1][northMap.StartPointX].Clone();
            var SouthPoint = map[0][southMap.EndPointX].Clone();
            var WestPoint = map[westMap.EndPointY][0].Clone();
            var EastPoint = map[eastMap.StartPointY][map[0].Length - 1].Clone();


            DrawCorridorHorizontal(
                map,
                (map[0].Length) - TilesBeside(map, map[0].Length, eastMap.StartPointY, direction.left, TileType.Rock),
                map[0].Length,
                eastMap.StartPointY,
                TileType.Rock,
                TileType.Dirt,
                TerrainType.YellowCave,
                TerrainType.BlackCaste);

            //Draw Corridor connecting map and SouthMap
            DrawCorridorHVertical(
                map,
                0,
                TilesBeside(map, southMap.EndPointX, 0, direction.up, TileType.Rock),
                southMap.EndPointX,
                TileType.Rock,
                TileType.Dirt,
                TerrainType.YellowCave,
                TerrainType.BlackCaste);

            MapChecker checker = new MapChecker(this, map);
            EastToSouth = checker.CheckMap(SouthPoint, EastPoint, direction.up);


            //Draw Corridor connecting map and WestMap
            DrawCorridorHorizontal(
                map,
                0,
                TilesBeside(map,0, westMap.EndPointY, direction.right, TileType.Rock),
                westMap.EndPointY,
                TileType.Rock,
                TileType.Dirt,
                TerrainType.YellowCave,
                TerrainType.BlackCaste);
            
            checker = new MapChecker(this, map);
            SouthToWest = checker.CheckMap(SouthPoint, WestPoint, direction.right);
            

            //Draw Corridor connecting map and NorthMap
            DrawCorridorHVertical(
                map,
                (map.Length) - TilesBeside(map, northMap.StartPointX, map.Length, direction.down, TileType.Rock),
                map.Length,
                northMap.StartPointX,
                TileType.Rock,
                TileType.Dirt,
                TerrainType.YellowCave,
                TerrainType.BlackCaste);
            checker = new MapChecker(this,map);
            WestToNorth = checker.CheckMap(WestPoint, NorthPoint, direction.right);
    }


    public void DrawStartAndEndPoints(TileStruct[][] map, ref int startX, ref int startY, ref int endX, ref int endY)
    {

        if (map[0].Length >= map.Length)
        {



            var closest = ClosestToBorderX(map, TileType.Dirt);
            DrawCorridorHorizontal(map, 0, closest.X, closest.Y, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.YellowCave);

            startY = closest.Y;
            startX = 0;


            var closest2 = ClosestToBorderXReverse(map, TileType.Dirt);
            //SpawnStartExitPoint(ClosestToBorderX(TileType.Dirt), false);
            DrawCorridorHorizontal(map, map[0].Length, closest2.X, closest2.Y, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.YellowCave);

            endX = map[0].Length - 1;
            endY = closest2.Y;



        }
        else
        {
            var closest = ClosestToBorderY(map,TileType.Dirt);
            DrawCorridorHVertical(map, 0, closest.Y, closest.X, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.YellowCave);

            startX = closest.X;
            startY = 0;


            var closest2 = ClosestToBorderYReverse(map, TileType.Dirt);
            //SpawnStartExitPoint(ClosestToBorderX(TileType.Dirt), false);
            DrawCorridorHVertical(map, map.Length, closest2.Y, closest2.X, TileType.Rock, TileType.Dirt, TerrainType.BlackCaste, TerrainType.YellowCave);

            endX = closest2.X;
            endY = map.Length - 1;



        }


        
    }







    public void PlaceBorders(TileType type, TileStruct[][] map)
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                if (y == 0 || y == map.Length - 1)
                {
                    map[y][x].Type = type;
                }
                else if (x == 0 || x == map[0].Length - 1)
                {
                    map[y][x].Type = type;
                }

            }
        }
    }


    public void TrimMap(TileStruct[][] map)
    {
        foreach (var y2 in map)
        {

            foreach (var x2 in y2)
            {
                int surroundingTiles = 0;
                var currentTile = x2;
                var x = currentTile.X;
                var y = currentTile.Y;

                if (GetTileData(map, x - 1, y).Type == TileType.Dirt) { surroundingTiles++; };

                if (GetTileData(map, x + 1, y).Type == TileType.Dirt) { surroundingTiles++; }
                if (GetTileData(map, x, y + 1).Type == TileType.Dirt) { surroundingTiles++; }
                if (GetTileData(map, x, y - 1).Type == TileType.Dirt) { surroundingTiles++; }

                if (surroundingTiles == 3 && currentTile.Type == TileType.Rock)
                {
                    currentTile.Type = TileType.Dirt;

                }
            }
        }


    }


    /// <summary>
    /// Gets the tile data at specified map coordinates
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>TileStruct.</returns>
    public TileStruct GetTileData(TileStruct[][] map, int x, int y)
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



    public int TilesBeside(TileStruct[][] map, int x, int y, direction direction, TileType type)
    {
        int count = 0;
        if (direction == direction.up)
        {
            y++;
            while (GetTileData(map, x, y).Type == type)
            {
                count++;
                y++;
            }
            return count;

        }
        else if (direction == direction.down)
        {
            y--;
            while (GetTileData(map, x, y).Type == type)
            {
                count++;
                y--;
            }
            return count;
        }
        else if (direction == direction.left)
        {
            x--;
            while (GetTileData(map, x, y).Type == type)
            {
                count++;
                x--;
            }
            return count;
        }
        else
        {
            x++;
            while (GetTileData(map, x, y).Type == type)
            {
                count++;
                x++;
            }
            return count;
        }
    }


    public TileStruct ClosestToBorderX(TileStruct[][] map, TileType type)
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


    public TileStruct ClosestToBorderXReverse(TileStruct[][] map, TileType type)
    {
        int bestValue = 0;
        TileStruct bestMatch = null;
        for (int y = map.Length - 1; y >= 0; y--)
        {
            for (int x = map[0].Length - 1; x >= 0; x--)
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

    public TileStruct ClosestToBorderY(TileStruct[][] map, TileType type)
    {
        int bestValue = map.Length;
        TileStruct bestMatch = null;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                if (map[y][x].Type == type && y < bestValue)
                {
                    bestValue = y;
                    bestMatch = map[y][x];
                }
            }
        }
        return bestMatch;
    }

    public TileStruct ClosestToBorderYReverse(TileStruct[][] map, TileType type)
    {
        int bestValue = 0;
        TileStruct bestMatch = null;
        for (int y = map.Length - 1; y >= 0; y--)
        {
            for (int x = map[0].Length - 1; x >= 0; x--)
            {
                if (map[y][x].Type == type && y > bestValue)
                {
                    bestValue = map[y][x].Y;
                    bestMatch = map[y][x];
                }
            }
        }
        return bestMatch;
    }


    public void DrawCorridorHorizontal(TileStruct[][] map, int x1, int x2, int y, TileType wallType, TileType floorType, TerrainType wallTerrainType, TerrainType floorTerrainType)
    {
        int length = Mathf.Abs(x1 - x2) + 1;

        int distanceA = x1 - x2;
        int distanceB = x1 - x2;


        for (int i = 0; i < length; i++)
        {
            TileStruct pointA;
            if (distanceA <= 0)
            {
                pointA = GetTileData(map, x1 + i, y);
            }
            else
            {
                pointA = GetTileData(map, x1 - i, y);
            }
            pointA.Type = floorType;
            pointA.SetFloor(floorTerrainType);



        }


    }

    public void DrawCorridorHVertical(TileStruct[][] map, int y1, int y2, int x, TileType wallType, TileType floorType, TerrainType wallTerrainType, TerrainType floorTerrainType)
    {
        int length = Mathf.Abs(y1 - y2) + 1;

        int distanceA = y1 - y2;
        int distanceB = y1 - y2;


        for (int i = 0; i < length; i++)
        {
            TileStruct pointA;
            if (distanceA <= 0)
            {
                pointA = GetTileData(map, x, y1 + i);
            }
            else
            {
                pointA = GetTileData(map, x, y1 - i);
            }
            pointA.Type = floorType;
            pointA.SetFloor(floorTerrainType);



        }


    }

}



