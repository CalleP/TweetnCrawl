using UnityEngine;
using System.Collections;

class Minigun : MachineGun {
    public Minigun()
    {
        altCoolDown = 0.1f;
        coolDown = 0.04f;
        AltSpread = 4;
        type = WeaponTypes.MiniGun;
        altFireEnabled = false;
    }

    public override void AltFire()
    {
        base.Fire();
    }
    
}
