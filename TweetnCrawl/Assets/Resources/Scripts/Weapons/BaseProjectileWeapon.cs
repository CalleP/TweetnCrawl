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





    






}

