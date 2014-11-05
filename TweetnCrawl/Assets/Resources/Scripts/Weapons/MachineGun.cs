using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class MachineGun : Revolver
{
    public MachineGun() : base()
    {
            
        coolDown = 0.1f;
        Spread = 8;
    }
        
}
