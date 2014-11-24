using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum EnemyTypes 
{
    Basic,
    Splitter,
    Hive,
    Teleporter,
    Melee,
    MeleeSplitter


}

public class MapPopulator : MonoBehaviour {




    public static Dictionary<EnemyTypes, GameObject> EnemyDict = new Dictionary<EnemyTypes, GameObject> 
    { 
        {EnemyTypes.Basic, (GameObject)Resources.Load("BasicEnemy")},
        {EnemyTypes.Hive, (GameObject)Resources.Load("HiveEnemy")},
        {EnemyTypes.Splitter, (GameObject)Resources.Load("RangedSplitter")},
        {EnemyTypes.Teleporter, (GameObject)Resources.Load("TeleporterEnemy")},
        {EnemyTypes.Melee, (GameObject)Resources.Load("Enemy")},
        {EnemyTypes.MeleeSplitter, (GameObject)Resources.Load("Splitter")}
    };

    public Dictionary<string, GameObject> Enemies = new Dictionary<string, GameObject>();
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void PopulateMap(TileMap map)
    {
        //var obj = Instantiate(randomType)
        //obj.randomizer.randomize(terrainType)
        var currentType = map.map[map.map.Length / 2][map.map[0].Length / 2].FloorTerrainType;
    }
}
