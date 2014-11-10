using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class MachineGun : Revolver
{
    public MachineGun() : base()
    {
        fireSound = null;
        coolDown = 0.12f;
        Spread = 8;
    }
        
}
