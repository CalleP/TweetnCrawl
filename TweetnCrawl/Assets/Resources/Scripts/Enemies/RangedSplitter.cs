using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Collections;

class RangedSplitter : BasicEnemy {

    public GameObject SpawnedEnemy;
    public int AmountOfSpawns;
	// Update is called once per frame
    public override void Update()
    {

        base.Update();
        //if the enemy followers health reaches 0 remove him from the game.
        if (health <= 0)
        {
            print("Split");

            Split();
            Destroy((Follower as Transform).gameObject);
        }

    }

    public void Split()
    {
        for (int i = 0; i < AmountOfSpawns; i++)
        {
            Instantiate(SpawnedEnemy, transform.position, transform.rotation);
            
        }
    }
}
