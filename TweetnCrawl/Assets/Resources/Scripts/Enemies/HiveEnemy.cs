using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class HiveEnemy : BasicEnemy
{
    public GameObject SpawnEffect;
    public int MaxSpawns;
    public GameObject SpawnedMonster;
    public float SpawnCooldown;

    private float time4;

    private List<GameObject> monsters = new List<GameObject>();


    void Start()
    {

        baseReferences();
        //InvokeRepeating("patrol", 1f, 2f); 

    }

    public override void Update()
    {
        base.Update();
        int count = 0;
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] != null)
	        {
                count++;
	        }

        }
        if (time4 <= Time.time)
        {
;
            if (count <= MaxSpawns)
            {
                var obj = (GameObject)Instantiate(SpawnEffect, transform.position, Quaternion.identity);
                obj.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
                Spawn();
                time4 = Time.time + SpawnCooldown;
                Debug.Log("Spawning");
            }

        }
    }

    public void Spawn()
    {

        var obj = (GameObject)Instantiate(SpawnedMonster, transform.position, Quaternion.identity);
        monsters.Add(obj);
    }
       


}


