using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class RaveGun : MachineGun
{
    public RaveGun() : base()
    {
        altDamage = 25;
        altCoolDown = 0.1f;
        coolDown = 0.04f;
        AltSpread = 2;
        type = WeaponTypes.RaveGun;
        PauseOnHit = 0.008f;
        shell = null;
        
    }

    public override void Fire()
    {
        AltFire();
    }
    public override void AltFire()
    {
        switch (UnityEngine.Random.Range(0,4))
        {
            case 0:
                LaserPrefabString = "YellowLazor";
                break;
            case 1:
                LaserPrefabString = "GreenLazor";
                break;
            case 2:
                LaserPrefabString = "Lazor";
                break;
            case 3:
                LaserPrefabString = "RedLazor";
                break;
            default:
                break;
        }
        base.AltFire();
    }
}
