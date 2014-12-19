using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class HashtagGrid
{
    public string[][] Grid;
    public string CurrentHashtag
    {
        get { return Grid[x][y]; }
        private set{ CurrentHashtag = value;}
    }

    private int x;
    private int y;

    public int Width;
    public int Height;

    public HashtagGrid(List<HashTagSet> hashtags, int dimension)
    {

        Grid = new string[dimension][];
        for (int i = 0; i < dimension; i++)
        {
            Grid[i] = new string[dimension];
        }

        int count = 0;
        for (int x = 0; x < dimension; x++)
        {
            for (int y = 0; y < dimension; y++)
            {
                Grid[x][y] = hashtags[count].Value;
                count++;
            }
        }

        Height = dimension;
        Width = dimension;

    }

    public void MoveWest()
    {
        MoveHorizontal(-1);
    }
    public void MoveEast()
    {
        MoveHorizontal(1);
    }
    public void MoveNorth()
    {
        MoveVertical(1);
    }
    public void MoveSouth()
    {
        MoveVertical(-1);
    }

    //values can only be between -1 and 1
    private void MoveHorizontal(int distance)
    {
        var location = x + distance;
        if (location < 0)
        {
            x = Width - 1;
        }
        else if (location > Width-1)
        {
            x = 0;
        }
        else
        {
            x = location;
        }
    
    }

    //values can only be between -1 and 1
    private void MoveVertical(int distance)
    {
        var location = y + distance;
        if (location < 0)
        {
            y = Height - 1;
        }
        else if (location > Height - 1)
        {
            y = 0;
        }
        else
        {
            y = location;
        }
    }

    public void MoveToHashtag(string hashtag)
    {
        for (int x = 0; x < Grid.Length; x++)
        {
            for (int y = 0; y < Grid[0].Length; y++)
            {
                if (Grid[x][y] == hashtag)
                {
                    this.x = x;
                    this.y = y;
                    return;
                }
            }
        }
    }


    public string WestNeighbour()
    {
        MoveWest();
        var output = CurrentHashtag;
        MoveEast();
        return output;
    }
    public string EastNeighbour()
    {
        MoveEast();
        var output = CurrentHashtag;
        MoveWest();
        return output;
    }
    public string NorthNeighbour()
    {
        MoveNorth();
        var output = CurrentHashtag;
        MoveSouth();
        return output;
    }
    public string SouthNeighbour()
    {
        MoveSouth();
        var output = CurrentHashtag;
        MoveNorth();
        return output;
    }


}

