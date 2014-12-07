using System;
using System.Collections;
using UnityEngine;


class RailGun : MachineGun
{
    public RailGun() : base()
    {
        SemiAuto = true;
        LaserPrefabString = "GreenLazor";
        altCoolDown = 0.7f;
        altDamage = 150;
        type = WeaponTypes.RailGun;
        ShakeMagnitude = 5;
        ShakeDuration = 1f;
        LaserWidth = 2.2f;
    
    }

    public override void Fire()
    {
        AltFire();

    }


}

   
