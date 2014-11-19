using UnityEngine;
using System.Collections;


class DualRevolvers : Revolver {

    bool firstFire;
    public DualRevolvers()
    {
        firstFire = true;
        coolDown = 0.15f;
        altCoolDown = 0.15f;
        type = WeaponTypes.revolver;
    }

    public override void Fire()
    {
        if (firstFire)
        {
            timeStamp = Time.time;
        }
        if (canFire())
        {
            firstFire = false;
            base.Fire();
        }

    }

    public override void AltFire()
    {
        if (!firstFire)
        {
            timeStamp = Time.time;
        }
        if (canFire())
        {
            firstFire = true;
            base.Fire();

        }

    }

}
