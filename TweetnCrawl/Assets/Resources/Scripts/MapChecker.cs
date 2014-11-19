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


    public TileStruct[][] map;
    TileStruct startPoint;
    TileStruct endPoint;
    MapHandler generator;
    public MapChecker(MapHandler generator, TileStruct[][] map)
    {
        this.map = map;
        this.generator = generator;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
    }


    //checks if it is possible to reach the endpoint from the startPoint

    private direction currentDirection;
    private int x;
    private int y;
    public bool CheckMap(TileStruct startPoint, TileStruct EndPoint, direction startingDirection)
    {
        currentDirection = startingDirection;
        x = startPoint.X;
        y = startPoint.Y;
        bool firstTime = true;
        int count = 0;

        while (x != EndPoint.X || y != EndPoint.Y)
        {
            

            if (MapHandler.GetTileData(map,x, y).Type == TileType.None)
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
                //generator.GetTileData(map,x, y).test = (int)currentDirection;
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
                    //generator.GetTileData(map,x, y).SetBoth(TerrainType.BlackCaste);
                }
                else
                {
                    
                    turnLeft();
                    goForward();
                   //generator.GetTileData(map,x, y).SetBoth(TerrainType.BlackCaste);
                }
                


                //generator.GetTileData(map, x, y).SetBoth(TerrainType.BlackCaste);
                
                
            }

            //MapHandler.GetTileData(map, x, y).test = (int)currentDirection;
            firstTime = false;
            //generator.GetTileData(map, x, y).SetBoth(TerrainType.BlackCaste);
            count++;

        }

        return true;
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
            if (MapHandler.GetTileData(map, x, y - 1).Type == TileType.Rock) return true; 
        }
        else if (currentDirection == direction.right)
        {
            if (MapHandler.GetTileData(map, x + 1, y).Type == TileType.Rock) return true;
        }
        else if (currentDirection == direction.up)
        {
            if (MapHandler.GetTileData(map, x, y + 1).Type == TileType.Rock) return true;
        }
        else if (currentDirection == direction.left)
        {
            if (MapHandler.GetTileData(map, x - 1, y).Type == TileType.Rock) return true;
        }  

        return false;
    }

    private bool canGoForward()
    {
        if (currentDirection == direction.down)
        {
            if (MapHandler.GetTileData(map, x + 1, y).Type == TileType.Rock && !isTileInFrontBlocking()) return true;
        } 
        else if (currentDirection == direction.right)
        {
            if (MapHandler.GetTileData(map, x, y + 1).Type == TileType.Rock && !isTileInFrontBlocking()) return true;
        }
        else if (currentDirection == direction.up)
        {
            if (MapHandler.GetTileData(map, x - 1, y).Type == TileType.Rock && !isTileInFrontBlocking()) return true;
        }
        else if (currentDirection == direction.left)
        {
            if (MapHandler.GetTileData(map, x, y - 1).Type == TileType.Rock && !isTileInFrontBlocking()) return true;
        }

        return false;
    }
}

