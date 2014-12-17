using UnityEngine;
using System.Collections;


class DualRevolvers : BaseProjectileWeapon {

    public DualRevolvers()
    {
        coolDown = 0f;
        altCoolDown = 0.15f;
        type = WeaponTypes.revolver;
        ShakeMagnitude = 0.1f;
        ShakeDuration = 0.1f;
        BulletSpeed = 100;
        AmmoCost = 0;
    }


    public override void AltFire()
    {
        base.Fire();
    }


}
