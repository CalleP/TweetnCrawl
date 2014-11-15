using UnityEngine;
using System.Collections;


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
    BlackCaste
}




/// <summary>
/// The data a tile is composed of
/// </summary>
public class TileStruct{

    public string WallTerrainType = "YellowCave";
    public string FloorTerrainType = "YellowCave";
    public TileType Type;
    public int Y;
    public int X;
    public int surroundingDirts = 0;


    //remove when finished testing
    public int test = 0;

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
        SetFloor(type);
        SetWall(type);
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
  
}
