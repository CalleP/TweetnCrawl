using UnityEngine;
using System.Collections;

class Minigun : MachineGun {
    public Minigun()
    {
        altCoolDown = 0.1f;
        coolDown = 0.04f;
        AltSpread = 4;
        type = WeaponTypes.Minigun;
        altFireEnabled = false;
        PauseOnHit = 0f;
        BulletSpeed = 60;
        Spread = 11;
    }

    public override void AltFire()
    {
        base.Fire();
    }
    
}
