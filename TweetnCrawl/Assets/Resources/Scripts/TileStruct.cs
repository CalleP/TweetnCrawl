using UnityEngine;
using System.Collections;
using System;


public enum TileType
{
    Dirt,
    Rock,
    Wood,
    None
}

public enum TerrainType
{ 
    YellowCave,
    BlackCaste,
    RedCave,
    GreyCave,
    BlackCave,
    BlueCastle
}

public enum DecorType
{
    None,
    Grass,
    Rock 
}



/// <summary>
/// The data a tile is composed of
/// </summary>
public class TileStruct{

    public bool Visited = false;

    public bool Debug = false;

    public string WallTerrainType = "YellowCave";
    public string FloorTerrainType = "YellowCave";

    public int SurroundingRocks = 0;

    public TerrainType terrainType;
    public DecorType DecorType = DecorType.None;
    public TileType Type;
    public int Y;
    public int X;
    public int surroundingDirts = 0;

    public int test = 0;


    public TileStruct(int x, int y, TileType type, TerrainType terrainType, DecorType decor)
    {
        this.DecorType = decor;
        SetBoth(terrainType);
        this.X = x;
        this.Y = y;
        this.Type = type;
    }

    public TileStruct(int x, int y, TileType type, TerrainType terrainType)
    {
        SetBoth(terrainType);
        this.X = x;
        this.Y = y;
        this.Type = type;
    }


    public TileStruct(int x, int y, TileType type) {
        this.X = x;
        this.Y = y;
        this.Type = type;
    }


    public void SetWall(TerrainType WallTerrain)
    {
        switch (WallTerrain)
        {
            case TerrainType.YellowCave:
                WallTerrainType = "YellowCave";
                break;
            case TerrainType.BlackCaste:
                WallTerrainType = "BlackCastle";
                break;
            default:
                break;
        }
    }

    public void SetFloor(TerrainType floorTerrain)
    {
        switch (floorTerrain)
        {
            case TerrainType.YellowCave:
                FloorTerrainType = "YellowCave";
                break;
            case TerrainType.BlackCaste:
                FloorTerrainType = "BlackCastle";
                break;
            default:
                break;
        }
    }


    public void SetBoth(TerrainType type)
    {
        Enum.GetName(typeof(TerrainType), type);
        terrainType = type;
        switch (type)
        {
            case TerrainType.YellowCave:
                FloorTerrainType = "YellowCave";
                WallTerrainType = "YellowCave";
                break;
            case TerrainType.BlackCaste:
                FloorTerrainType = "BlackCave";
                WallTerrainType = "BlackCastle";
                break;
            case TerrainType.RedCave:
                FloorTerrainType = "RedCave";
                WallTerrainType = "RedCave";
                break;
            case TerrainType.GreyCave:
                FloorTerrainType = "BlackCave";
                WallTerrainType = "GreyCave";
                break;
            case TerrainType.BlackCave:
                FloorTerrainType = "BlackCave";
                WallTerrainType = "BlackCave";
                break;
            case TerrainType.BlueCastle:
                FloorTerrainType = "BlackCave";
                WallTerrainType = "BlueCastle";
                break;

            default:
                break;
        }
    }

    public string GetTerrainType()
    {
        if (Type == TileType.Rock)
        {
            return WallTerrainType;

        }
        else
        {
            return FloorTerrainType + "Floor";
        }
    }

    public string GetDecorType()
    {
        var value = Enum.GetName(DecorType.GetType(), DecorType);
        if (value == "None")
        {
            value = "";
        }
        return value;
    }

    public TileStruct Clone()
    {
        var outTile = new TileStruct(X,Y,Type);

        outTile.FloorTerrainType = FloorTerrainType;
        outTile.WallTerrainType = FloorTerrainType;

        return outTile;
    }


    public static int UnityUnitToTileUnit(float position)
    {
        return (int)(position / 3.2f);
    }

    public static float TileUnityToUnityUnit(int position)
    {
        return position * 3.2f;
    }

    public static TerrainType getRandomTerrainType(int seed)
    {
        System.Random random = new System.Random(seed);
        Array values = Enum.GetValues(typeof(TerrainType));
        return (TerrainType)values.GetValue(random.Next(values.Length));
    }
}
