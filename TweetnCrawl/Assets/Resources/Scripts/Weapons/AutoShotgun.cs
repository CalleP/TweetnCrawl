using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class AutoShotgun : ShotgunWeapon
{
    public AutoShotgun() : base()
    {
        type = WeaponTypes.AutoShotgun;
        coolDown = 0.25f;
        Spread = 15;
        ProjectileAmount = 5;
        SemiAuto = false;
        altFireEnabled = false;
        
    }
}
