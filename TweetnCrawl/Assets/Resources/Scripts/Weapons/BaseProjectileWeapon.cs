using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class BaseProjectileWeapon : BaseWeapon
{
    UnityEngine.Object projectile;
    public List<BaseProjectile> projectiles = new List<BaseProjectile>();

    public float BulletSpeed = 60f;
    public float VelocityVariation = 1f;

    public int Spread;
    public int AltSpread;

    public virtual void Start()
    { 
    
    }

    public BaseProjectileWeapon()
    {

        fireSound = Resources.Load<AudioClip>("Sounds/RevolverFireSound");
        reloadSound = null;
        Spread = 3;
        BulletSpeed = 60f;
        coolDown = 0.15f;
        SemiAuto = true;
    }

    public BaseProjectile SpawnProjectile(Vector3 direction, Quaternion rotation, string projectilePrefab, float speed)
    {
        var prefab = Resources.Load(projectilePrefab);
        var projectile = (GameObject)Instantiate(prefab, wielder.transform.position, rotation);
        var projectileScript = projectile.GetComponent<BaseProjectile>();

        projectileScript.Init(direction, rotation, speed + UnityEngine.Random.Range(-VelocityVariation,VelocityVariation), damage, this);

        return projectileScript;

    }

    public BaseProjectile SpawnProjectile(float speed, string projectilePrefab)
    {
        var objectPos = wielder.transform.position;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        Quaternion outRotation = Quaternion.Euler(new Vector3(0, 0, angle));
  



        var outDirection = mousePos - wielder.transform.position;
        
        return SpawnProjectile(outDirection, outRotation, projectilePrefab, speed);

    }
        System.Random rand = new System.Random();





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

