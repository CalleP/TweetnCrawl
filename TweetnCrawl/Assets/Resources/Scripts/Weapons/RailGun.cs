using System;
using System.Collections;
using UnityEngine;


class RailGun : MachineGun
{
    public RailGun() : base()
    {
        Shell = null;
        SemiAuto = true;
        LaserPrefabString = "GreenLazor";
        altCoolDown = 0.7f;
        altDamage = 150;
        type = WeaponTypes.RailGun;
        ShakeMagnitude = 5;
        ShakeDuration = 1f;
        LaserWidth = 2.2f;
        PauseOnHit = 0.1f;
        AmmoCost = 5;
    
    }

    public override void Fire()
    {
        AltFire();

    }


}

   
