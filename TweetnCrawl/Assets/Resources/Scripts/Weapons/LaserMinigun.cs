using UnityEngine;
using System.Collections;

class LaserMinigun : MachineGun {
    public LaserMinigun()
    {
        altCoolDown = 0.1f;
        coolDown = 0.04f;
        AltSpread = 4;
        type = WeaponTypes.laserMiniGun;
    }
    
}
