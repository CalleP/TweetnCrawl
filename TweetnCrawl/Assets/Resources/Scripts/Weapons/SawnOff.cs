using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class SawnOff : Shotgun
{
    public SawnOff() : base()
    {
        type = WeaponTypes.SawnOff;
        ProjectileAmount = 12;
        Spread = 30;
        BulletSpeed = 45;

        VelocityVariation = 10;

    }
}

