using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class PickupWeapon : PickupBase
{
    private static int test = 0;
    void Start()
    {
        if (test == 0) { Item = new ShotgunWeapon(); }
        else if(test == 1)
        {
            Item = new OrbitalWeapon();
        }
        test++;
    }

    protected override void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            coll.gameObject.GetComponent<Inventory>().PickUp(this);
            Destroy(gameObject);
        }
    }
}

