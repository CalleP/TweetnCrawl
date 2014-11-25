using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct EnemySpriteKey
{

    public int Frame;
    public EnemyTypes EnemyType;
    public TerrainType Variation;


    public EnemySpriteKey(int frame, EnemyTypes enemyType, TerrainType variation)
    {
        this.Frame = frame;
        this.EnemyType = enemyType;
        this.Variation = variation;
    }
}

public class EnemyRandomizer : Animator {

    public static Dictionary<EnemySpriteKey, Sprite> TerrainTypeDict = InitRandomizer();
   

    public static Dictionary<EnemySpriteKey, Sprite> InitRandomizer()
    {
        var dict = new Dictionary<EnemySpriteKey, Sprite>();

        //BlackCave
        dict.Add(new EnemySpriteKey(0, EnemyTypes.Basic, TerrainType.BlackCave), GetSprite("Characters/Undead_0", "Undead_0_16"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Basic, TerrainType.BlackCave), GetSprite("Characters/Undead_1", "Undead_1_16"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Teleporter, TerrainType.BlackCave), GetSprite("Characters/Undead_0", "Undead_0_39"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Teleporter, TerrainType.BlackCave), GetSprite("Characters/Undead_1", "Undead_1_39"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Splitter, TerrainType.BlackCave), GetSprite("Characters/Undead_0", "Undead_0_18"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Splitter, TerrainType.BlackCave), GetSprite("Characters/Undead_1", "Undead_1_18"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Hive, TerrainType.BlackCave), GetSprite("Characters/Undead_0", "Undead_0_38"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Hive, TerrainType.BlackCave), GetSprite("Characters/Undead_1", "Undead_1_38"));

        //BlackCastle
        dict.Add(new EnemySpriteKey(0, EnemyTypes.Basic, TerrainType.BlackCaste), GetSprite("Characters/Undead_0", "Undead_0_5"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Basic, TerrainType.BlackCaste), GetSprite("Characters/Undead_1", "Undead_1_5"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Teleporter, TerrainType.BlackCaste), GetSprite("Characters/Undead_0", "Undead_0_0"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Teleporter, TerrainType.BlackCaste), GetSprite("Characters/Undead_1", "Undead_1_0"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Splitter, TerrainType.BlackCaste), GetSprite("Characters/Undead_0", "Undead_0_2"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Splitter, TerrainType.BlackCaste), GetSprite("Characters/Undead_1", "Undead_1_2"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Hive, TerrainType.BlackCaste), GetSprite("Characters/Undead_0", "Undead_0_38"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Hive, TerrainType.BlackCaste), GetSprite("Characters/Undead_1", "Undead_1_38"));

        ////BlueCastle
        dict.Add(new EnemySpriteKey(0, EnemyTypes.Basic, TerrainType.BlueCastle), GetSprite("Characters/Undead_0", "Undead_0_16"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Basic, TerrainType.BlueCastle), GetSprite("Characters/Undead_1", "Undead_1_16"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Teleporter, TerrainType.BlueCastle), GetSprite("Characters/Undead_0", "Undead_0_39"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Teleporter, TerrainType.BlueCastle), GetSprite("Characters/Undead_1", "Undead_1_39"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Splitter, TerrainType.BlueCastle), GetSprite("Characters/Undead_0", "Undead_0_18"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Splitter, TerrainType.BlueCastle), GetSprite("Characters/Undead_1", "Undead_1_18"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Hive, TerrainType.BlueCastle), GetSprite("Characters/Undead_0", "Undead_0_38"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Hive, TerrainType.BlueCastle), GetSprite("Characters/Undead_1", "Undead_1_38"));
        
        //GreyCave
        dict.Add(new EnemySpriteKey(0, EnemyTypes.Basic, TerrainType.GreyCave), GetSprite("Characters/Undead_0", "Undead_0_16"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Basic, TerrainType.GreyCave), GetSprite("Characters/Undead_1", "Undead_1_16"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Teleporter, TerrainType.GreyCave), GetSprite("Characters/Undead_0", "Undead_0_39"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Teleporter, TerrainType.GreyCave), GetSprite("Characters/Undead_1", "Undead_1_39"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Splitter, TerrainType.GreyCave), GetSprite("Characters/Undead_0", "Undead_0_18"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Splitter, TerrainType.GreyCave), GetSprite("Characters/Undead_1", "Undead_1_18"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Hive, TerrainType.GreyCave), GetSprite("Characters/Undead_0", "Undead_0_38"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Hive, TerrainType.GreyCave), GetSprite("Characters/Undead_1", "Undead_1_38"));


        //IceCave
        dict.Add(new EnemySpriteKey(0, EnemyTypes.Basic, TerrainType.IceCave), GetSprite("Characters/Undead_0", "Undead_0_16"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Basic, TerrainType.IceCave), GetSprite("Characters/Undead_1", "Undead_1_16"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Teleporter, TerrainType.IceCave), GetSprite("Characters/Undead_0", "Undead_0_39"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Teleporter, TerrainType.IceCave), GetSprite("Characters/Undead_1", "Undead_1_39"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Splitter, TerrainType.IceCave), GetSprite("Characters/Undead_0", "Undead_0_18"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Splitter, TerrainType.IceCave), GetSprite("Characters/Undead_1", "Undead_1_18"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Hive, TerrainType.IceCave), GetSprite("Characters/Undead_0", "Undead_0_38"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Hive, TerrainType.IceCave), GetSprite("Characters/Undead_1", "Undead_1_38"));


        //RedCave
        dict.Add(new EnemySpriteKey(0, EnemyTypes.Basic, TerrainType.RedCave), GetSprite("Characters/Undead_0", "Undead_0_16"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Basic, TerrainType.RedCave), GetSprite("Characters/Undead_1", "Undead_1_16"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Teleporter, TerrainType.RedCave), GetSprite("Characters/Undead_0", "Undead_0_39"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Teleporter, TerrainType.RedCave), GetSprite("Characters/Undead_1", "Undead_1_39"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Splitter, TerrainType.RedCave), GetSprite("Characters/Undead_0", "Undead_0_18"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Splitter, TerrainType.RedCave), GetSprite("Characters/Undead_1", "Undead_1_18"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Hive, TerrainType.RedCave), GetSprite("Characters/Undead_0", "Undead_0_38"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Hive, TerrainType.RedCave), GetSprite("Characters/Undead_1", "Undead_1_38"));

        //YellowCave
        dict.Add(new EnemySpriteKey(0, EnemyTypes.Basic, TerrainType.YellowCave), GetSprite("Characters/Undead_0", "Undead_0_16"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Basic, TerrainType.YellowCave), GetSprite("Characters/Undead_1", "Undead_1_16"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Teleporter, TerrainType.YellowCave), GetSprite("Characters/Undead_0", "Undead_0_39"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Teleporter, TerrainType.YellowCave), GetSprite("Characters/Undead_1", "Undead_1_39"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Splitter, TerrainType.YellowCave), GetSprite("Characters/Undead_0", "Undead_0_18"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Splitter, TerrainType.YellowCave), GetSprite("Characters/Undead_1", "Undead_1_18"));

        dict.Add(new EnemySpriteKey(0, EnemyTypes.Hive, TerrainType.YellowCave), GetSprite("Characters/Undead_0", "Undead_0_38"));
        dict.Add(new EnemySpriteKey(1, EnemyTypes.Hive, TerrainType.YellowCave), GetSprite("Characters/Undead_1", "Undead_1_38"));


        return dict;


    }

     static Sprite GetSprite(string TexturePath, string spriteName)
     {



         Sprite[] sprites = Resources.LoadAll<Sprite>(TexturePath);
         

         foreach (var item in sprites)
         {
             if (item.name == spriteName)
             {
                 return item;
             }
         }
         return null;
     }

     private BaseEnemy enemy;
     
     public override void Start()
     {
         base.Start();
         enemy = GetComponent<BaseEnemy>();

         //RandomizeFrames(enemy.EnemyType, enemy.terrainType);
     }

    public void RandomizeFrames(EnemyTypes typeOfEnemy, TerrainType associatedTType)
    {

        sprites[0] = TerrainTypeDict[new EnemySpriteKey(0, typeOfEnemy, associatedTType)];
        sprites[1] = TerrainTypeDict[new EnemySpriteKey(1, typeOfEnemy, associatedTType)];

    }
}
