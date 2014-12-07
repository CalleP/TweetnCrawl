using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ShotgunWeapon : BaseProjectileWeapon
{

    public int ProjectileAmount;
    public int Spread;



    System.Random rand = new System.Random();

    public ShotgunWeapon()
    {
        coolDown = 0.7f;
        BulletSpeed = 60f;
        Spread = 20;
        ProjectileAmount = 8;
        damage = 35;
        SemiAuto = true;
        type = WeaponTypes.shotgun;
        altCoolDown = 0.7f;

    }

    public override void Start()
    {
        base.Start();
    }



    public override void Fire()
    {
        var mousePos = AimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var rotation = Vector3.Angle(wielder.transform.position, mousePos);
        var velocity = Vector3.up;
        


        if (canFire())
        {
            projectiles = new List<BaseProjectile>();

            for (int i = 0; i < ProjectileAmount; i++)
            {
                projectiles.Add(SpawnProjectile(BulletSpeed, "ShotgunProjectile"));
                projectiles[i].transform.Rotate(new Vector3(0, 0, rand.Next(0-Spread,Spread)));
            }



            base.Fire();
            
        }

    }

    public override void AltFire()
    {
        var mousePos = AimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var rotation = Vector3.Angle(wielder.transform.position, mousePos);
        var velocity = Vector3.up;
        if (canFire() && altFireEnabled) 
        {
            
            projectiles.Add(SpawnProjectile(BulletSpeed, "ShotgunAltProjectile"));
            projectiles[projectiles.Count-1].transform.Rotate(new Vector3(0, 0, 0));
            
            base.AltFire();
        }

    }




}
