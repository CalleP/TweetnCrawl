using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Revolver : BaseProjectileWeapon
{

    System.Random rand = new System.Random();

    public int Spread;
    public int AltSpread;
    public Revolver() 
    {
        
        fireSound = Resources.Load<AudioClip>("Sounds/RevolverFireSound");
        reloadSound = null;
        Spread = 3;
        BulletSpeed = 60f;
        coolDown = 0.15f;
        SemiAuto = true;
    }


    public override void Fire()
    {
        var mousePos = AimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var rotation = Vector3.Angle(wielder.transform.position, mousePos);
        var velocity = Vector3.up;



        if (canFire())
        {
           
                var projectile = SpawnProjectile(BulletSpeed, "RevolverProjectile");
                projectile.transform.Rotate(new Vector3(0, 0, rand.Next(0 - Spread, Spread)));



            base.Fire();

        }

    }
}
