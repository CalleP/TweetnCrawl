using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//We followed the tutorial http://csharpcodewhisperer.blogspot.se/2013/07/Rouge-like-dungeon-generation.html for the cellular automata implementation
public class MapGen
{
    System.Random rand = new System.Random();

    public int[,] Map;

    public int MapWidth { get; set; }
    public int MapHeight { get; set; }
    public int PercentAreWalls { get; set; }
    public TerrainType TerrainType;
    private int seed;

    public MapGen(int mapWidth, int mapHeight, int wallPercentage, TerrainType terrainType, int seed)
    {
        this.seed = seed;
        rand = new System.Random(seed);

        this.TerrainType = terrainType;
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

    public MapGen(int mapWidth, int mapHeight, int[,] map, int percentWalls = 40)
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


 



        DrawStartAndEndPoints(convertedArray, ref startX, ref startY, ref endX, ref endY);






        MapChecker checker = new MapChecker(convertedArray);


        if (checker.IsPointReachable(
            GetTileData(convertedArray, startX, startY),
            GetTileData(convertedArray, endX, endY),
            direction.right))
        {
            //FloodFill(convertedArray, startX, startY, TileType.Dirt, TileType.Rock);

            //RemoveUnconnectedChambers(convertedArray, convertedArray[startY][startX], convertedArray[endY][endX]);
            BreadthFirst(convertedArray, convertedArray[startY][startX]);

            Sprinkle(convertedArray, 62, DecorType.Grass);
            Sprinkle(convertedArray, 68, DecorType.Rock);


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
            Sprinkle(convertedArray, 62, DecorType.Grass);
            Sprinkle(convertedArray, 68, DecorType.Rock);

            BreadthFirst(convertedArray, convertedArray[westMap.EndPointY][0]);

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
                    convertedArray[y][x] = new TileStruct(x, y, TileType.Dirt, TerrainType);
                }
                else if (Map[x, y] == 1)
                {
                    convertedArray[y][x] = new TileStruct(x, y, TileType.Rock, TerrainType);
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
            TerrainType,
            TerrainType);

        //Draw Corridor connecting map and SouthMap
        DrawCorridorHVertical(
            map,
            0,
            TilesBeside(map, southMap.EndPointX, 0, direction.up, TileType.Rock),
            southMap.EndPointX,
            TileType.Rock,
            TileType.Dirt,
            TerrainType,
            TerrainType);

        MapChecker checker = new MapChecker(map);
        EastToSouth = checker.IsPointReachable(SouthPoint, EastPoint, direction.up);


        //Draw Corridor connecting map and WestMap
        DrawCorridorHorizontal(
            map,
            0,
            TilesBeside(map, 0, westMap.EndPointY, direction.right, TileType.Rock),
            westMap.EndPointY,
            TileType.Rock,
            TileType.Dirt,
            TerrainType,
            TerrainType);

        checker = new MapChecker(map);
        SouthToWest = checker.IsPointReachable(SouthPoint, WestPoint, direction.right);


        //Draw Corridor connecting map and NorthMap
        DrawCorridorHVertical(
            map,
            (map.Length) - TilesBeside(map, northMap.StartPointX, map.Length, direction.down, TileType.Rock),
            map.Length,
            northMap.StartPointX,
            TileType.Rock,
            TileType.Dirt,
            TerrainType,
            TerrainType);
        checker = new MapChecker(map);
        WestToNorth = checker.IsPointReachable(WestPoint, NorthPoint, direction.right);
    }


    public void DrawStartAndEndPoints(TileStruct[][] map, ref int startX, ref int startY, ref int endX, ref int endY)
    {

        if (map[0].Length >= map.Length)
        {



            var closest = ClosestToBorderX(map, TileType.Dirt);
            DrawCorridorHorizontal(map, 0, closest.X, closest.Y, TileType.Rock, TileType.Dirt, TerrainType, TerrainType);

            startY = closest.Y;
            startX = 0;


            var closest2 = ClosestToBorderXReverse(map, TileType.Dirt);
            //SpawnStartExitPoint(ClosestToBorderX(TileType.Dirt), false);
            DrawCorridorHorizontal(map, map[0].Length, closest2.X, closest2.Y, TileType.Rock, TileType.Dirt, TerrainType, TerrainType);

            endX = map[0].Length - 1;
            endY = closest2.Y;



        }
        else
        {
            var closest = ClosestToBorderY(map, TileType.Dirt);
            DrawCorridorHVertical(map, 0, closest.Y, closest.X, TileType.Rock, TileType.Dirt, TerrainType, TerrainType);

            startX = closest.X;
            startY = 0;


            var closest2 = ClosestToBorderYReverse(map, TileType.Dirt);
            //SpawnStartExitPoint(ClosestToBorderX(TileType.Dirt), false);
            DrawCorridorHVertical(map, map.Length, closest2.Y, closest2.X, TileType.Rock, TileType.Dirt, TerrainType, TerrainType);

            endX = closest2.X;
            endY = map.Length - 1;



        }



    }



    public void Sprinkle(TileStruct[][] map, int percentage, DecorType decor)
    {
        MapGen handler = new MapGen(MapWidth, MapHeight, percentage, TerrainType, seed);
        handler.BlankMap();
        handler.RandomFillMap();

        handler.MakeCaverns();

        var overlayingMap = handler.GetAsTileStructArr();

        for (int y = 0; y < overlayingMap.Length; y++)
        {
            for (int x = 0; x < overlayingMap[0].Length; x++)
            {
                if (overlayingMap[y][x].Type == TileType.Dirt)
                {
                    map[y][x].DecorType = decor;
                }
            }
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
                int surroundingRocks = 0;
                int surroundingTiles = 0;
                var currentTile = x2;
                var x = currentTile.X;
                var y = currentTile.Y;

                var left = GetTileData(map, x - 1, y);
                var right = GetTileData(map, x + 1, y);
                var up = GetTileData(map, x, y + 1);
                var down = GetTileData(map, x, y - 1);


                if (left.Type == TileType.Dirt) { surroundingTiles++; };

                if (right.Type == TileType.Dirt) { surroundingTiles++; }
                if (up.Type == TileType.Dirt) { surroundingTiles++; }
                if (down.Type == TileType.Dirt) { surroundingTiles++; }

                if (surroundingTiles == 3 && currentTile.Type == TileType.Rock)
                {
                    currentTile.Type = TileType.Dirt;

                }


                if (left.Type == TileType.Rock) { surroundingRocks++; };

                if (right.Type == TileType.Rock) { surroundingRocks++; }
                if (up.Type == TileType.Rock) { surroundingRocks++; }
                if (down.Type == TileType.Rock) { surroundingRocks++; }
                currentTile.SurroundingRocks = surroundingRocks;

            }
        }


    }


    /// <summary>
    /// Gets the tile data at specified map coordinates
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>TileStruct.</returns>
    public static TileStruct GetTileData(TileStruct[][] map, int x, int y)
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
                if (map[y][x].Type == type && x < bestValue)
                {
                    bestValue = x;
                    bestMatch = map[y][x];
                }
            }
        }
        List<TileStruct> bestMatches = new List<TileStruct>();
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i][bestValue].Type == TileType.Dirt)
            {
                bestMatches.Add(map[i][bestValue]);
            }
        }
        return bestMatches[rand.Next(0, bestMatches.Count - 1)];
    }


    public TileStruct ClosestToBorderXReverse(TileStruct[][] map, TileType type)
    {
        int bestValue = 0;
        //List<TileStruct> bestMatches = new List<TileStruct>();
        TileStruct bestMatch = null;
        for (int y = map.Length - 1; y >= 0; y--)
        {
            for (int x = map[0].Length - 1; x >= 0; x--)
            {
                if (map[y][x].Type == type && x > bestValue)
                {
                    bestValue = map[y][x].X;
                    bestMatch = map[y][x];



                    //if (bestMatches.Count == 0 || bestValue < bestMatches[bestMatches.Count - 1].X)
                    //{
                    //    bestMatches = new List<TileStruct>();
                    //    bestMatches.Add(bestMatch);
                    //}
                    //else if (bestValue == bestMatches[bestMatches.Count - 1].X)
                    //{
                    //    bestMatches.Add(bestMatch);
                    //}
                }
            }
        }
        List<TileStruct> bestMatches = new List<TileStruct>();
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i][bestValue].Type == TileType.Dirt)
            {
                bestMatches.Add(map[i][bestValue]);
            }
        }
        return bestMatches[rand.Next(0, bestMatches.Count-1)];
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

        List<TileStruct> bestMatches = new List<TileStruct>();
        for (int i = 0; i < map[0].Length; i++)
        {
            if (map[bestValue][i].Type == TileType.Dirt)
            {
                bestMatches.Add(map[bestValue][i]);
            }
        }
        return bestMatches[rand.Next(0, bestMatches.Count - 1)];

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
        List<TileStruct> bestMatches = new List<TileStruct>();
        for (int i = 0; i < map[0].Length; i++)
        {
            if (map[bestValue][i].Type == TileType.Dirt)
            {
                bestMatches.Add(map[bestValue][i]);
            }
        }
        return bestMatches[rand.Next(0, bestMatches.Count - 1)];
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
            pointA.SetBoth(wallTerrainType);



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
            pointA.SetBoth(wallTerrainType);



        }


    }



    public static void FloodFill(TileStruct[][] map, int x, int y, TileType targetType, TileType replacementType)
    {
        /*
        1: stuff the start pixel into a queue, note its color. note it as added.
        2: begin picking a pixel off the queue. If it's similar to the start pixel:
           2: put all its neighbours into the queue
              for each added pixel, note it's added. if already noted for a pixel, don't 
              add it anymore.
           3: color it with the destination color.
        3: nonempty => jump back to 2
        4: empty => we are finished

         */

        List<string> list = new List<string>();
        Queue<TileStruct> q = new Queue<TileStruct>();
        q.Enqueue(map[y][x]);

        while (q.Count != 0)
        {
            var tile = q.Dequeue();
            if (tile.Type == targetType)
            {
                tile.Type = replacementType;

                var west = MapGen.GetTileData(map, tile.X - 1, tile.Y);
                if (!list.Contains(west.X + "," + west.Y)) { q.Enqueue(west); list.Add(west.X + "," + west.Y); }
                //west.Type = replacementType;

                var east = MapGen.GetTileData(map, tile.X + 1, tile.Y);
                if (!list.Contains(east.X + "," + east.Y)) { q.Enqueue(east); list.Add(east.X + "," + east.Y); }
                //east.Type = replacementType;


                var north = MapGen.GetTileData(map, tile.X, tile.Y + 1);
                if (!list.Contains(north.X + "," + north.Y)) { q.Enqueue(north); list.Add(north.X + "," + north.Y); }
                //north.Type = replacementType;

                var south = MapGen.GetTileData(map, tile.X, tile.Y - 1);
                if (!list.Contains(south.X + "," + south.Y)) { q.Enqueue(north); list.Add(south.X + "," + south.Y); }
                //south.Type = replacementType;

            }
        }




        






    }

    private Dictionary<int[], TileStruct> dict = new Dictionary<int[], TileStruct>();
    private Stack<TileStruct> stack = new Stack<TileStruct>();
    public void BreadthFirst(TileStruct[][] map, TileStruct start)
    {
        stack.Push(start);

        while (stack.Count != 0)
        {
            var tile = stack.Pop();


            tile.Visited = true;


            var west = MapGen.GetTileData(map, tile.X - 1, tile.Y);
            var east = MapGen.GetTileData(map, tile.X + 1, tile.Y);
            var north = MapGen.GetTileData(map, tile.X, tile.Y + 1);
            var south = MapGen.GetTileData(map, tile.X, tile.Y - 1);

            if (west.Visited == false && west.Type == TileType.Dirt) stack.Push(west);
            if (east.Visited == false && east.Type == TileType.Dirt) stack.Push(east);
            if (north.Visited == false && north.Type == TileType.Dirt) stack.Push(north);
            if (south.Visited == false && south.Type == TileType.Dirt) stack.Push(south);



        }

        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                var tile = map[y][x];
                if (tile.Type == TileType.Dirt && tile.Visited == false)
                {
                    tile.Type = TileType.Rock;
                }
            }
        }
    
    }

    //public void RemoveUnconnectedChambers(TileStruct[][] map, TileStruct start, TileStruct end)
    //{


    //    end.Type = TileType.Rock;
    //    end.test2 = "Visited";
    //    MapChecker outerTiles = new MapChecker(map);

        
    //    outerTiles.CheckMap(start, end, direction.right);

    //    end.Type = TileType.Dirt;
    //    //Fails the check intentionally
    //    var visitedTiles = outerTiles.VisitedTiles;

    //    foreach (var item in visitedTiles)
    //    {
    //        item.test2 = "Visited";
    //    }

    //    foreach (var y2 in map)
    //    {

    //        foreach (var x2 in y2)
    //        {
    //            int surroundingTiles = 0;
    //            var currentTile = x2;
    //            var x = currentTile.X;
    //            var y = currentTile.Y;

    //            var left = GetTileData(map, x - 1, y);
    //            var right = GetTileData(map, x + 1, y);
    //            var up = GetTileData(map, x, y + 1);
    //            var down = GetTileData(map, x, y - 1);


    //            if (left.Type == TileType.Rock) { surroundingTiles++; };

    //            if (right.Type == TileType.Rock) { surroundingTiles++; }
    //            if (up.Type == TileType.Rock) { surroundingTiles++; }
    //            if (down.Type == TileType.Rock) { surroundingTiles++; }

    //            if ((surroundingTiles != 0 && currentTile.Type == TileType.Dirt) && (currentTile.test2 != "Visited"))
    //            {
    //                currentTile.Type = TileType.Rock;

    //            }

    //        }
    //    }
    //}
}










