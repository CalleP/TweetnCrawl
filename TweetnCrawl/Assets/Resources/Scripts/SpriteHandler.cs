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

    public static Sprite GetTexture(TileStruct tile, TileMap map)
    {



        if (tile.Type == TileType.Rock)
        {
            return getSpriteWithName(tile.WallTerrainType + getNumber(
            tile.WallTerrainType,
            map.GetTileData(tile.X - 1, tile.Y).GetTerrainType(),
            map.GetTileData(tile.X + 1, tile.Y).GetTerrainType(),
            map.GetTileData(tile.X, tile.Y + 1).GetTerrainType(),
            map.GetTileData(tile.X, tile.Y - 1).GetTerrainType(),
            map.GetTileData(tile.X - 1, tile.Y + 1).GetTerrainType(),
            map.GetTileData(tile.X + 1, tile.Y + 1).GetTerrainType(),
            map.GetTileData(tile.X - 1, tile.Y - 1).GetTerrainType(),
            map.GetTileData(tile.X + 1, tile.Y - 1).GetTerrainType()
            ),


            tile.Type);

        }
        if (tile.Type == TileType.Dirt)
        {
            return getSpriteWithName(tile.GetTerrainType() + getNumberFloors(
            tile.GetTerrainType(),
            map.GetTileData(tile.X - 1, tile.Y).GetTerrainType(),
            map.GetTileData(tile.X + 1, tile.Y).GetTerrainType(),
            map.GetTileData(tile.X, tile.Y + 1).GetTerrainType(),
            map.GetTileData(tile.X, tile.Y - 1).GetTerrainType(),
            map.GetTileData(tile.X - 1, tile.Y + 1).GetTerrainType(),
            map.GetTileData(tile.X + 1, tile.Y + 1).GetTerrainType(),
            map.GetTileData(tile.X - 1, tile.Y - 1).GetTerrainType(),
            map.GetTileData(tile.X + 1, tile.Y - 1).GetTerrainType()
            ),


            tile.Type);
        }

        return null;

    }

    private static string getNumberFloors(string center, string left, string right, string up, string down,
        string upLeft, string upRight, string downLeft, string downRight)
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
        //      +           
        if (center != up && center == down && center != left && center != right) return "_4";

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

    private static string getHorizontalNumber(string center, string upLeft, string upRight, string downLeft, string downRight)
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



        //    - + -
        //    + c +
        //    + + +
        if (center != upLeft && center != upRight && center == downLeft && center == downRight) return "T_2";

        //    + + -
        //    + c +
        //    + + -
        if (center == upLeft && center != upRight && center == downLeft && center != downRight) return "T_4";

        //    + + +
        //    + c +
        //    - + -
        if (center == upLeft && center == upRight && center != downLeft && center != downRight) return "T_8";

        //    - + +
        //    + c +
        //    - + +
        if (center != upLeft && center == upRight && center != downLeft && center == downRight) return "T_6";


        //    - + -
        //    + c +
        //    - + -
        if (center != upLeft && center != upRight && center != downLeft && center != downRight) return "T_6";




        return "T_7";
    }

    private static string getNumber(string center, string left, string right, string up, string down,
    string upLeft, string upRight, string downLeft, string downRight)
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


        ////HighPrio

        ////  ++-
        ////  +c-
        ////  -+-
        //if (center == upLeft && center == up && center != upRight && center == left && center != right && center != downLeft && center == down && center != downRight) return "T_6";


        ////  --+
        ////  +c+
        ////  ++-
        //if (center != upLeft && center != up && center == upRight && center == left && center == right && center == downLeft && center == down && center != downRight) return "T_6";

        ////  ++-
        ////  +c-
        ////  -++
        //if (center == upLeft && center == up && center != upRight && center == left && center != right && center != downLeft && center == down && center == downRight) return "T_6";

        ////  ++-
        ////  -c+
        ////  -+-
        //if (center == upLeft && center == up && center != upRight && center != left && center == right && center != downLeft && center == down && center == downRight) return "T_4";

        ////  +--
        ////  +c+
        ////  -++
        //if (center == upLeft && center != up && center != upRight && center == left && center == right && center != downLeft && center == down && center == downRight) return "T_8";

        ////  +-+
        ////  +c+
        ////  -++
        //if (center == upLeft && center != up && center == upRight && center == left && center == right && center != downLeft && center == down && center == downRight) return "T_8";

        ////  +-+
        ////  +c+
        ////  -++
        //if (center == upLeft && center != up && center == upRight && center == left && center == right && center != downLeft && center == down && center == downRight) return "T_8";



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
        if (center == up && center == down && center == left && center != right) return getDirectional1(center, upLeft, upRight, downLeft,downRight);

        //      +  
        //     -c+
        //      -
        if (center == up && center != down && center != left && center == right) return "_1";

        //      +  
        //     -c+
        //      +
        if (center == up && center == down && center != left && center == right) return getDirectional2(center, upLeft, upRight, downLeft, downRight);

        //DOWN
        //      -
        //     -c-
        //      +            
        if (center != up && center == down && center != left && center != right) return "_4";

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
        if (center == up && center != down && center == left && center == right) return getDirectional4(center, upLeft, upRight, downLeft, downRight);

        //      -
        //     +c-
        //      +
        if (center != up && center == down && center == left && center != right) return "_9";

        //      -
        //     +c+
        //      +
        if (center != up && center == down && center == left && center == right) return getDirectional3(center, upLeft, upRight, downLeft, downRight);

        //right
        //      -
        //     -c+
        //      -
        if (center != up && center != down && center != left && center == right) return "_1";

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

    //      +
    //     +c-
    //      +
    private static string getDirectional1(string center, string upLeft, string upRight, string downLeft, string downRight)
    { 
        //     ++?
        //     +c-
        //     ++?
        if (center == upLeft && center == downLeft) return "_4";
        else return "T_6";


    }

    //      +
    //     -c+
    //      +
    private static string getDirectional2(string center, string upLeft, string upRight, string downLeft, string downRight)
    {
        //     ?++
        //     -c+
        //     ?++
        if (center == upRight && center == downRight) return "_4";
        else return "T_4";


    }

    //      -
    //     +c+
    //      +
    private static string getDirectional3(string center, string upLeft, string upRight, string downLeft, string downRight)
    {
        //     ?-?
        //     +c+
        //     +++
        if (center == downLeft && center == downRight) return "_8";
        else return "T_8";


    }

    //      +
    //     +c+
    //      -
    private static string getDirectional4(string center, string upLeft, string upRight, string downLeft, string downRight)
    {
        //     +++
        //     +c+
        //     ?-?
        if (center == upRight && center == upLeft) return "_8";
        else return "T_2";


    }
        /*
        ............................................________ 
    ....................................,.-'"...................``~., 
    .............................,.-"..................................."-., 
    .........................,/...............................................":, 
    .....................,?......................................................, 
    .................../...........................................................,} 
    ................./......................................................,:`^`..} 
    .............../...................................................,:"........./ 
    ..............?.....__.........................................:`.........../ 
    ............./__.(....."~-,_..............................,:`........../ 
    .........../(_...."~,_........"~,_....................,:`........_/ 
    ..........{.._$;_......"=,_......."-,_.......,.-~-,},.~";/....} 
    ...........((.....*~_......."=-._......";,,./`..../"............../ 
    ...,,,___.`~,......"~.,....................`.....}............../ 
    ............(....`=-,,.......`........................(......;_,,-" 
    ............/.`~,......`-...................................../ 
    .............`~.*-,.....................................|,./.....,__ 
    ,,_..........}.>-._...................................|..............`=~-, 
    .....`=~-,__......`,................................. 
    ...................`=~-,,.,............................... 
    ................................`:,,...........................`..............__ 
    .....................................`=-,...................,%`>--==`` 
    ........................................_..........._,-%.......` 
    ..................................., 
     */



    public static Sprite getSpriteWithName(string spriteName, TileType type)
    {
        
        switch (type)
        {
            case TileType.Dirt:
                var test = Array.IndexOf(NamesFloors, spriteName);
                if (test == -1||(test < 0 || test >= NamesFloors.Length))
                {
                    Debug.Log("test");
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

    public static Sprite Get2FrameAnimation(Sprite[] sprites1, Sprite[] sprites2, string spriteName, bool firstFrame)
    {

        
        var Names = new string[sprites1.Length];
        for (int i = 0; i < Names.Length; i++)
        {
            Names[i] = sprites1[i].name;
        }

        var Names2 = new string[sprites2.Length];
        for (int i = 0; i < Names2.Length; i++)
        {
            Names2[i] = sprites2[i].name;
        }
       if (firstFrame)
	   {
	        return sprites1[Array.IndexOf(Names, spriteName)];
	   }
       return sprites2[Array.IndexOf(Names2, spriteName)];

    }



}
