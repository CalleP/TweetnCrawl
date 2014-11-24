using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRandomizer : Animator {

    public static Dictionary<string, Sprite> TerrainTypeDict = new Dictionary<string, Sprite>();


    public List<Texture2D> sprites;

     static EnemyRandomizer()
    {

        //TerrainTypeDict.Add("TerrainType, Basic", (Sprite)Resources.Load<>);


    }

    public void RandomizeFrames(TerrainType type)
    { 
        
    }
}
