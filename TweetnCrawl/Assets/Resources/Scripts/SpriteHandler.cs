using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpriteHandler : MonoBehaviour {

    public enum SpriteType
    {
        Wall,
        Floor

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    static SpriteHandler()
    {


        NamesFloors = new string[Floors.Length];
        for (int ii = 0; ii < NamesFloors.Length; ii++)
        {
            NamesFloors[ii] = Floors[ii].name;
        }

        NamesWalls = new string[Walls.Length];
        for (int ii = 0; ii < NamesWalls.Length; ii++)
        {
            NamesWalls[ii] = Walls[ii].name;
        }




    }

    public static string[] NamesWalls;
    public static string[] NamesFloors;

    public static Sprite[] Walls = Resources.LoadAll<Sprite>("Wall");
    public static Sprite[] Floors = Resources.LoadAll<Sprite>("Floor");

    public static Sprite GetTexture(string name, TileStruct tile, TileMap map)
    {



        if (tile.Type == TileType.Rock)
        {
            return getSpriteWithName(name + getNumber(
            tile.Type,
            map.GetTileData(tile.X - 1, tile.Y).Type,
            map.GetTileData(tile.X + 1, tile.Y).Type,
            map.GetTileData(tile.X, tile.Y + 1).Type,
            map.GetTileData(tile.X, tile.Y - 1).Type,
            map.GetTileData(tile.X - 1, tile.Y + 1).Type,
            map.GetTileData(tile.X + 1, tile.Y + 1).Type,
            map.GetTileData(tile.X - 1, tile.Y - 1).Type,
            map.GetTileData(tile.X + 1, tile.Y - 1).Type
            ),


            tile.Type);

        }
        if (tile.Type == TileType.Dirt)
        {
            return getSpriteWithName(name + getNumberFloors(
            tile.Type,
            map.GetTileData(tile.X - 1, tile.Y).Type,
            map.GetTileData(tile.X + 1, tile.Y).Type,
            map.GetTileData(tile.X, tile.Y + 1).Type,
            map.GetTileData(tile.X, tile.Y - 1).Type,
            map.GetTileData(tile.X - 1, tile.Y + 1).Type,
            map.GetTileData(tile.X + 1, tile.Y + 1).Type,
            map.GetTileData(tile.X - 1, tile.Y - 1).Type,
            map.GetTileData(tile.X + 1, tile.Y - 1).Type
            ),


            tile.Type);
        }

        return null;

    }

    private static string getNumberFloors(TileType center, TileType left, TileType right, TileType up, TileType down,
        TileType upLeft, TileType upRight, TileType downLeft, TileType downRight)
    {


        //      -
        //     -c-
        //      -
        if (center != up && center != down && center != left && center != right) return "Y_5";

        //      +
        //     +c+
        //      +
        if (center == up && center == down && center == left && center == right) return "_5";

        //      +
        //     -c-
        //      +
        if (center == up && center == down && center != left && center != right) return "Y_2";

        //      -
        //     +c+
        //      -
        if (center != up && center != down && center == left && center == right) return "X_2";


        //----------------------------------------------------------------------------------//
        // UP
        //      +
        //     -c-
        //      -
        if (center == up && center != down && center != left && center != right) return "Y_1";

        //      +
        //     +c-
        //      -
        if (center == up && center != down && center == left && center != right) return "_3";

        //      +
        //     +c-
        //      +
        if (center == up && center == down && center == left && center != right) return "_6";

        //      +  
        //     -c+
        //      -
        if (center == up && center != down && center != left && center == right) return "_1";

        //      +  
        //     -c+
        //      +
        if (center == up && center == down && center != left && center == right) return "_4";

        //DOWN
        //      -
        //     -c-
        //      +             TODO: here
        if (center != up && center == down && center != left && center != right) return "Y_3";

        //      -
        //     +c-
        //      +
        if (center != up && center == down && center == left && center != right) return "_9";

        //      -
        //     -c+
        //      +
        if (center != up && center == down && center != left && center == right) return "_7";

        // Left
        //      -
        //     +c-
        //      -
        if (center != up && center != down && center == left && center != right) return "X_3";

        //      +
        //     +c-
        //      -
        if (center == up && center != down && center == left && center != right) return "_3";
        //      +
        //     +c+
        //      -
        if (center == up && center != down && center == left && center == right) return "_2";

        //      -
        //     +c-
        //      +
        if (center != up && center == down && center == left && center != right) return "_9";

        //      -
        //     +c+
        //      +
        if (center != up && center == down && center == left && center == right) return "_8";

        //right
        //      -
        //     -c+
        //      -
        if (center != up && center != down && center != left && center == right) return "X_1";

        //      -
        //     -c+
        //      +
        if (center != up && center == down && center != left && center == right) return "_7";

        //      +
        //     -c+
        //      -
        if (center == up && center != down && center != left && center == right) return "_1";

        return "_5";



    }

    private static string getHorizontalNumber(TileType center, TileType upLeft, TileType upRight, TileType downLeft, TileType downRight)
    {
        //    + + +
        //    + c +
        //    + + -
        if (center == upLeft && center == upRight && center == downLeft && center != downRight) return "_7";


        //    + + +
        //    + c +
        //    - + +
        if (center == upLeft && center == upRight && center != downLeft && center == downRight) return "_9";

        //    - + +
        //    + c +
        //    + + +
        if (center != upLeft && center == upRight && center == downLeft && center == downRight) return "_3";


        //    + + -
        //    + c +
        //    + + +
        if (center == upLeft && center != upRight && center == downLeft && center == downRight) return "_1";

        //    + + -
        //    + c +
        //    - + +
        if (center == upLeft && center != upRight && center != downLeft && center == downRight) return "T_5";

        //    - + +
        //    + c +
        //    + + -
        if (center != upLeft && center == upRight && center == downLeft && center != downRight) return "T_5";


        return "T_7";
    }

    private static string getNumber(TileType center, TileType left, TileType right, TileType up, TileType down,
    TileType upLeft, TileType upRight, TileType downLeft, TileType downRight)
    {


        //      -
        //     -c-
        //      -
        if (center != up && center != down && center != left && center != right) return "_5";

        //      +
        //     +c+
        //      +
        if (center == up && center == down && center == left && center == right) return getHorizontalNumber(center, upLeft, upRight, downLeft, downRight);

        //      +
        //     -c-
        //      +
        if (center == up && center == down && center != left && center != right) return "_4";

        //      -
        //     +c+
        //      -
        if (center != up && center != down && center == left && center == right) return "_8";


        //----------------------------------------------------------------------------------//
        // UP
        //      +
        //     -c-
        //      -
        if (center == up && center != down && center != left && center != right) return "_5";

        //      +
        //     +c-
        //      -
        if (center == up && center != down && center == left && center != right) return "_3";

        //      +
        //     +c
        //      +
        if (center == up && center == down && center == left && center != right) return "_4";

        //      +  
        //     -c+
        //      -
        if (center == up && center != down && center != left && center == right) return "_1";

        //      +  
        //     -c+
        //      +
        if (center == up && center == down && center != left && center == right) return "_4";

        //DOWN
        //      -
        //     -c-
        //      +             TODO: here
        if (center != up && center == down && center != left && center != right) return "T_7";

        //      -
        //     +c-
        //      +
        if (center != up && center == down && center == left && center != right) return "_9";

        //      -
        //     -c+
        //      +
        if (center != up && center == down && center != left && center == right) return "_7";

        // Left
        //      -
        //     +c-
        //      -
        if (center != up && center != down && center == left && center != right) return "_3";

        //      +
        //     +c-
        //      -
        if (center == up && center != down && center == left && center != right) return "_3";
        //      +
        //     +c+
        //      -
        if (center == up && center != down && center == left && center == right) return "_8";

        //      -
        //     +c-
        //      +
        if (center != up && center == down && center == left && center != right) return "_9";

        //      -
        //     +c+
        //      +
        if (center != up && center == down && center == left && center == right) return "_8";

        //right
        //      -
        //     -c+
        //      -
        if (center != up && center != down && center != left && center == right) return "_5";

        //      -
        //     -c+
        //      +
        if (center != up && center == down && center != left && center == right) return "_7";

        //      +
        //     -c+
        //      -
        if (center == up && center != down && center != left && center == right) return "_3";

        return "_5";



    }



    public static Sprite getSpriteWithName(string spriteName, TileType type)
    {
        switch (type)
        {
            case TileType.Dirt:
                var test = Array.IndexOf(NamesFloors, spriteName);
                if (test < 0 || test >= NamesFloors.Length)
                {
                    ;
                }

                try
                {
                    return Floors[test];
                }
                catch (Exception)
                {
                    int test2 = test;
                    throw;
                }

            case TileType.Rock:
                return Walls[Array.IndexOf(NamesWalls, spriteName)];

        }

        return Walls[0];
    }


}
