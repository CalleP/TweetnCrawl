﻿using UnityEngine;
using System.Collections;


public enum TileType
{
    Dirt,
    Rock,
    Wood
}


/// <summary>
/// The data a tile is composed of
/// </summary>
public class TileStruct{

    public TileType Type;
    public int Y;
    public int X;

    public TileStruct(int x, int y, TileType type) {
        this.X = x;
        this.Y = y;
        this.Type = type;
    }
}
