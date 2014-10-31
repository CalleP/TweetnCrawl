using UnityEngine;
using System.Collections;

public class MapGen {
    
    
    //Tile based or not?
    private int _seed;
    private int _height;
    private int _width;

    private float _progress = 0.0f;

    public MapGen(int seed, int height, int width) {
        _seed = seed;
        _height = height;
        _width = width;
        generate();
    }



    //generates the level
    private void generate() { 
      //  Debug.Log(BSTgen.split(_height));
    }

    //returns progress of the generator from 0.0f to 1.0f
    public float progress() {
        return 0.0f;
    }
}
