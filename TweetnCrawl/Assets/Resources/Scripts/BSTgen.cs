using System;
using System.Collections;
using System.Collections.Generic;

class BSTgen
{

    //If current map is big enough
    //Decide if to crop horizontal or vertical
    //Cropmap in two random sized halves
    //createCorridor(leaf1, leaf2)
    //Recurse()
    //
    //If current map is too small
    //Create room()
    //
    //
    private static float RoomMaxEmptySpaceMod = 1.5f;
    private static Random rand = new Random();
    public static TileStruct[][] GenMap(TileStruct[][] arr, int roomMin, int roomMax, int corridorMin, int corridorMax, int roomMargin, bool first) {

        //TODO crops return wrong result
        //Checks if the size of the inputted array is big enough to hold a room
        if (arr[0].Length >= roomMax + (roomMargin * 2) && arr[0].Length <= (roomMax + (roomMargin * 2))*RoomMaxEmptySpaceMod)
        {
            return addRoom(arr, roomMin, roomMax, roomMargin, TileType.Rock, TileType.Dirt);
        }
        else
        {
            //var leftLeaf = rand.Next(45,55);
            //var rightLeaf = 100-leftLeaf;
            if (first) //(rand.Next(1, 2) == 1)
            {
                var leftLeafMap = TileMap.CropMap(arr, 0, 0, arr.Length, arr.Length / 2);
                var leftLeaf = GenMap(leftLeafMap, roomMin, roomMax, corridorMin, corridorMax, roomMargin, !first);

                var rightLeafMap = TileMap.CropMap(arr, arr.Length / 2, arr.Length, arr.Length, arr.Length);
                var rightLeaf = GenMap(rightLeafMap, roomMin, roomMax, corridorMin, corridorMax,roomMargin, !first);
                var mergedLeaves = horizontalMerge(leftLeaf, rightLeaf);
                var corridorSize = rand.Next(corridorMin,corridorMax);
                return TileMap.CropMap2(mergedLeaves, arr[0].Length/2 - corridorSize - roomMargin, arr.Length/2 - corridorSize - roomMargin,
                    arr[0].Length/2+corridorSize+roomMargin, arr.Length/2 + corridorSize + roomMargin,
                    TileType.Dirt);
                //    GenMap(), GenMap)
                //return addCorridor(GenMap(cropmapHorizontal(leftLeaf)), (GenMap(cropmapHorizontal(rightLeaf)),
            }
            else
            {
                var lowerLeafMap = TileMap.CropMap(arr, 0, 0, arr.Length, arr.Length / 2);
                var lowerLeaf = GenMap(lowerLeafMap, roomMin, roomMax, corridorMin, corridorMax, roomMargin, !first);

                var upperLeafMap = TileMap.CropMap(arr, arr.Length / 2, arr.Length, arr.Length, arr.Length);
                var upperLeaf = GenMap(upperLeafMap, roomMin, roomMax, corridorMin, corridorMax, roomMargin, !first);


                //return addCorridor(GenMap(cropmapVertical(leftLeaf)), (GenMap(cropmapVertical(rightLeaf)),
            }   
        }
        return null;
    }


    //direction zero vertical corridor
    private static TileStruct[][] addCorridor(TileStruct[][] arr1, TileStruct[][] arr2, int corridorMin, int corridorMax, int direction)
    {
        var corridorSize = rand.Next(corridorMin, corridorMax);
        if (direction == 0)
        {
         //   addRoom(TileMap.CropMap2(arr1, arr1[0].Length/2, arr1.Length/2 - corridorSize, arr1[0].Length, arr1.Length/2 + corridorSize), corridorMin, corridorMax, 0, TileType.Dirt, TileType.Dirt);
        }
        //arr1[connectingIndexes] make corridor
        //arr2[connectingIndexes] make corridor
        // merge(arr1 = arr2);
        return null;
    }


    public static TileStruct[][] horizontalMerge(TileStruct[][] arr1, TileStruct[][] arr2)
    {
        //Use lists?

        TileStruct[][] result;
        result = new TileStruct[arr1.Length + arr2.Length][];
        int count = 0;
        for (int i = 0; i < arr1.Length; i++)
        {
            result[count] = arr1[i];
            count++;
        }
        for (int i = 0; i < arr2.Length; i++)
        {
            result[count] = arr2[i];
            count++;
        }
        return result;
    }

    //Merge arr1 with arr2, arr1 is at top arr2 is at bottom
    public static TileStruct[][] verticalMerge(TileStruct[][] arr1, TileStruct[][] arr2)
    {
        var xOdd = arr2.Length % 2;


    
        //var yOdd = arr[0].Length % 2;


        TileStruct[][] result;
        result = new TileStruct[arr1.Length][];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new TileStruct[arr1[0].Length + arr2[0].Length];

            int count = 0;//TODO fix off by one
            for (int y = 0; y < (result[i].Length / 2); y++)
            {
                result[i][y] = arr1[i][count];
                result[i][y].Y = y;
                result[i][y].X = i;

               //var originalX = r
               result[i][y + arr1[0].Length] = arr2[i][y];
               var arr = result[i][y + arr1[0].Length];
               result[i][y + arr1[0].Length].Y += arr1.Length/2;
               //result[i][y + arr1[0].Length].X = result[i][y].X + arr1[0].Length;
               count++;

            }
            if (arr1[0].Length-arr2[0].Length == 1)
            {
                result[i][count] = arr2[i][arr2[i].Length - 1];
            }
            else if (arr1[0].Length-arr2[0].Length == -1)
            {
                result[i][count + arr1[0].Length + 1] = arr1[i][arr1[i].Length - 1];
                
            }
        }

        return result;
    }









    //private static TileStruct[][] tileListToArr(List<List<TileStruct>> list)
    //{
    //    TileStruct[][] outArr = new TileStruct[list.Count][];

    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        outArr[i] = list[i].ToArray();
    //    }

    //    return outArr;
    //}

    public static TileStruct[][] addRoom(TileStruct[][] arr, int roomMin, int roomMax, int roomMargin, TileType wall, TileType floor)
    {
        int roomSize = rand.Next(roomMin, roomMax);



        for (int i = 0; i < arr.Length; i++)
        {
            for (int y = 0; y < arr[i].Length; y++)
            {
                //Checks if the loop is in the position to add a floor tile
                if ((i < (arr.Length-1) - roomMargin && i > roomMargin) && y < (arr[i].Length-1) - roomMargin && y > roomMargin)
                {
                    arr[i][y].Type = floor;
                    //add to a room object
                }

                //Checks if the loop is in the position to add a Wall tile
                else if ((i <= (arr.Length-1) - roomMargin && i >= roomMargin) && y <= (arr[i].Length-1) - roomMargin && y >= roomMargin)
                {
                    arr[i][y].Type = wall;
                    //add to a room object
                }

            }
        }
        return arr;
    }



}
