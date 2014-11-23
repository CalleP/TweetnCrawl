using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class EnemyProjectile : BaseProjectile
{
    public override void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Wall")
        {
            spawnDeathAnim();
            Destroy(gameObject);
        }

        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<CharacterHealth>().receiveDamage(Damage);
            spawnDeathAnim();
            Destroy(gameObject);
        }
            
    }
}

