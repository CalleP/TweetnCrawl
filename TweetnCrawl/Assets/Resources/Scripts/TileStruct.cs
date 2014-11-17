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
    BlueCastle,
    IceCave
}




/// <summary>
/// The data a tile is composed of
/// </summary>
public class TileStruct{

    public string WallTerrainType = "YellowCave";
    public string FloorTerrainType = "YellowCave";

    public TerrainType terrainType;

    public TileType Type;
    public int Y;
    public int X;
    public int surroundingDirts = 0;


    //remove when finished testing
    public int test = 0;

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
                FloorTerrainType = "BlackCastle";
                WallTerrainType = "BlackCastle";
                break;
            case TerrainType.RedCave:
                FloorTerrainType = "RedCave";
                WallTerrainType = "RedCave";
                break;
            case TerrainType.GreyCave:
                FloorTerrainType = "BlackCastle";
                WallTerrainType = "GreyCave";
                break;
            case TerrainType.BlackCave:
                FloorTerrainType = "BlackCave";
                WallTerrainType = "BlackCave";
                break;
            case TerrainType.BlueCastle:
                FloorTerrainType = "BlackCastle";
                WallTerrainType = "BlueCastle";
                break;
            case TerrainType.IceCave:
                FloorTerrainType = "IceCave";
                WallTerrainType = "IceCave";
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

    public static TerrainType getRandomTerrainType()
    {
        System.Random random = new System.Random();
        Array values = Enum.GetValues(typeof(TerrainType));
        return (TerrainType)values.GetValue(random.Next(values.Length));
    }
}
