using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class MachineGun : Revolver
{
    public MachineGun() : base()
    {
        fireSound = Resources.Load<AudioClip>("Sounds/MachineGunSound");
        coolDown = 0.12f;
        Spread = 8;
    }
        
}
