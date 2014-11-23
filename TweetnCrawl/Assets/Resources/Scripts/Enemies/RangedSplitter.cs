using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Collections;

class RangedSplitter : BasicEnemy {

    public GameObject SpawnedEnemy;
    public int AmountOfSpawns;
    public GameObject SplitEffect;
    public float SplitDelay = 1f;
	// Update is called once per frame
    public override void Update()
    {

        base.Update();
        //if the enemy followers health reaches 0 remove him from the game.
        if (health <= 0)
        {
            print("Split");

            StartCoroutine(WaitAndSplit(SplitDelay));
            var obj = (GameObject)Instantiate(SplitEffect, transform.position, Quaternion.identity);
            obj.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));


        }

    }

    public void Split()
    {
        for (int i = 0; i < AmountOfSpawns; i++)
        {
            Instantiate(SpawnedEnemy, transform.position, transform.rotation);
            
        }
    }
    public IEnumerator WaitAndSplit(float waitTime)
    {

        Split();
        yield return new WaitForSeconds(waitTime);
        
        //Destroy((Follower as Transform).gameObject);
        yield return null;

    }
}
