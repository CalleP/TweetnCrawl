using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class PickupWeapon : PickupBase
{
    public enum weaponTypes
    {
        shotgun,
        machineGun,
        revolver,
        laserMiniGun
    }

    public weaponTypes selectedWeapon = weaponTypes.revolver;
    private static int test = 0;
    
    void Start()
    {
        
        if (selectedWeapon == weaponTypes.revolver)
        {
            Item = new DualRevolvers();
        }

        else if (selectedWeapon == weaponTypes.machineGun)
        {
            Item = new MachineGun();
        }

        else if (selectedWeapon == weaponTypes.shotgun)
        {
            Item = new ShotgunWeapon();
        }

        else if (selectedWeapon == weaponTypes.laserMiniGun)
        {
            Item = new LaserMinigun();
        }
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

