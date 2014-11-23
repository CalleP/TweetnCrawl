using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Collections;

class RangedSplitter : BasicEnemy {
	
	
	// Update is called once per frame
    public override void Update()
    {

        base.Update();
        //if the enemy followers health reaches 0 remove him from the game.
        if (health <= 0)
        {
            print("Split");
            Instantiate(Resources.Load("BasicEnemy"), transform.position, transform.rotation);
            Instantiate(Resources.Load("BasicEnemy"), transform.position, transform.rotation);
            Instantiate(Resources.Load("BasicEnemy"), transform.position, transform.rotation);
            Instantiate(Resources.Load("BasicEnemy"), transform.position, transform.rotation);
            //Instantiate(Resources.Load("Enemy"), transform.position.y + 2, transform.rotation);
            //Instantiate(Resources.Load("Enemy"), transform.position.y - 2, transform.rotation);
            Destroy((Follower as Transform).gameObject);
        }
    }
}
