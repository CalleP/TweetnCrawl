using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum direction
{
    up = 0,
    down = 1,
    left = 2,
    right = 3
}

class MapChecker
{


    TileMap map;
    TileStruct startPoint;
    TileStruct endPoint;
    public MapChecker(TileMap map)
    {
        this.map = map;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
    }


    //checks if it is possible to reach the endpoint from the startPoint

    private direction currentDirection;
    private int x;
    private int y;
    public bool CheckMap(TileStruct startPoint, TileStruct EndPoint)
    {
        currentDirection = direction.right;
        x = startPoint.X;
        y = startPoint.Y;
        bool firstTime = true;
        int count = 0;

        while (x != EndPoint.X || y != EndPoint.Y)
        {
            if (map.GetTileData(x, y).Type == TileType.None)
            {
                return false;
            }
            if ((x == startPoint.X &&  y == startPoint.Y) && !firstTime)
            {
                return false;
            }

            
            if (canGoForward())
            {
                goForward();
                map.GetTileData(x, y).test = (int)currentDirection;
            }
            else
            {

                if (isTileInFrontBlocking())
                {
                    while (isTileInFrontBlocking())
                    {
                        turnLeft();
                        if (!isTileInFrontBlocking())
                        {
                            
                        }
                        else
                        {
                            turnRight();
                            turnRight();
                        }
                      
                    }
                    
                    goForward();
                    map.GetTileData(x, y).SetBoth(TerrainType.BlackCaste);
                }
                else
                {
                    
                    turnLeft();
                    goForward();
                    map.GetTileData(x, y).SetBoth(TerrainType.BlackCaste);
                }
                


                map.GetTileData(x, y).SetBoth(TerrainType.BlackCaste);
                
                
            }

            map.GetTileData(x, y).test =  (int)currentDirection;
            firstTime = false;
            map.GetTileData(x, y).SetBoth(TerrainType.BlackCaste);
            count++;

        }

        return true;
    }

    public bool CheckHub(TileStruct Point1, TileStruct Point2, TileStruct Point3, TileStruct Point4)
    {
        if (CheckMap(Point1, Point2) && CheckMap(Point2,Point3) && CheckMap(Point3,Point4))
        {
            return true;
        }

        return false;
    }

    //private void HugWall()
    //{
    //    if      (currentDirection == direction.down)    while (canGoLeft()) GoLeft();
    //    else if (currentDirection == direction.right)   while (canGoDown()) GoDown();
    //    else if (currentDirection == direction.up)      while (canGoRight()) GoRight();
    //    else if (currentDirection == direction.left)    while (canGoUp()) GoUp();


    //}

    private void turnLeft()
    {
        if (currentDirection == direction.down) currentDirection = direction.right;
        else if (currentDirection == direction.right) currentDirection = direction.up;
        else if (currentDirection == direction.up) currentDirection = direction.left;
        else if (currentDirection == direction.left) currentDirection = direction.down;
    }
    private void turnRight()
    {
        if (currentDirection == direction.down) currentDirection = direction.left;
        else if (currentDirection == direction.right) currentDirection = direction.down;
        else if (currentDirection == direction.up) currentDirection = direction.right;
        else if (currentDirection == direction.left) currentDirection = direction.up;
    }

    private void goForward()
    {
        if (currentDirection == direction.down) GoDown();
        else if (currentDirection == direction.right) GoRight();
        else if (currentDirection == direction.up) GoUp();
        else if (currentDirection == direction.left) GoLeft();
    }

    private void GoLeft()
    {
        x = x - 1;
    }
    private void GoRight()
    {
        x = x + 1;
    }
    private void GoUp()
    {
        y=y+1;
    }
    private void GoDown()
    {
        y = y - 1;
    }

    private bool isTileInFrontBlocking()
    {
        if (currentDirection == direction.down)
        { 
            if (map.GetTileData(x, y - 1).Type == TileType.Rock) return true; 
        }
        else if (currentDirection == direction.right)
        {
            if (map.GetTileData(x + 1, y).Type == TileType.Rock) return true;
        }
        else if (currentDirection == direction.up)
        {
            if (map.GetTileData(x, y + 1).Type == TileType.Rock) return true;
        }
        else if (currentDirection == direction.left)
        {
            if (map.GetTileData(x - 1, y).Type == TileType.Rock) return true;
        }  

        return false;
    }

    private bool canGoForward()
    {
        if (currentDirection == direction.down)
        {
            if (map.GetTileData(x + 1, y).Type == TileType.Rock && !isTileInFrontBlocking()) return true;
        } 
        else if (currentDirection == direction.right)
        {
            if (map.GetTileData(x, y + 1).Type == TileType.Rock && !isTileInFrontBlocking()) return true;
        }
        else if (currentDirection == direction.up)
        {
            if (map.GetTileData(x - 1, y).Type == TileType.Rock && !isTileInFrontBlocking()) return true;
        }
        else if (currentDirection == direction.left)
        {
            if (map.GetTileData(x, y - 1).Type == TileType.Rock && !isTileInFrontBlocking()) return true;
        }

        return false;
    }
}

