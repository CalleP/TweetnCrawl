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
    }

    public override void Fire()
    {
        AltFire();

    }


}

   
