using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Launcher : Shotgun
{
    public Launcher() : base()
    {
        SemiAuto = false;
        altCoolDown = 0.4f;
        BulletSpeed = 45;
        type = WeaponTypes.Launcher;
    }
    public override void Fire()
    {
        AltFire();
    }
}

