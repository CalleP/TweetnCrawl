using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class MachineGun : BaseProjectileWeapon
{
    protected string LaserPrefabString;
    public float LaserWidth = 1.5f;
    private System.Random rand = new System.Random();
    public MachineGun() : base()
    {
        LaserPrefabString = "Lazor";
        fireSound = null;
        coolDown = 0.12f;
        altCoolDown = 0.6f;
        BulletSpeed = 100f;
        Spread = 8;
        altDamage = 50;
        SemiAuto = false;
        damage = 40;
        type = WeaponTypes.machineGun;
        PauseOnHit = 0f;
        Spread = 2;
        
    }

    public override void AltFire()
    {
        if (canFire() && altFireEnabled)
        {
            FireLaser();
        }

    }

    public void FireLaser()
    {
        var objectPos = wielder.transform.position;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos = new Vector3(mousePos.x + rand.Next(AltSpread * -1, AltSpread), mousePos.y + rand.Next(AltSpread * -1, AltSpread), wielder.transform.position.z);
        var hits = Physics2D.RaycastAll(wielder.transform.position, (mousePos - objectPos).normalized, 200f);

        var closestHit = 5000f;
        var farthestHit = 0f;
        GameObject bestMatch = null;
        RaycastHit2D hit = new RaycastHit2D();
        bool foundWall = false;
        for (int i = 0; i < hits.Length; i++)
        {

            if (hits[i].distance <= closestHit)
            {
                if (hits[i].collider.gameObject.tag == "Wall")
                {
                    float distance = (hits[i].point - (Vector2)wielder.transform.position).magnitude;
                    foundWall = true;
                    closestHit = distance;
                    bestMatch = hits[i].collider.gameObject;
                    hit = hits[i];

                }

            }
            if (hits[i].distance >= farthestHit && hits[i].collider.gameObject.tag != "Wall" && !foundWall)
            {
                float distance = (hits[i].point - (Vector2)wielder.transform.position).magnitude;
                farthestHit = distance;
                bestMatch = hits[i].collider.gameObject;
                hit = hits[i];
            }


        }

        bool hitEnemy = false;
        for (int i = 0; i < hits.Length; i++)
        {
            float distance = (hits[i].point - (Vector2)wielder.transform.position).magnitude;
            if (hits[i].collider.gameObject.tag == "Enemy" && distance <= closestHit)
            {
                hits[i].collider.gameObject.GetComponent<BaseEnemy>().receiveDamage(altDamage);

                hitEnemy = true;
            }
        }

        if (hitEnemy)
        {
            var obj = (GameObject)Instantiate(Resources.Load("OnHitEffect"), new Vector3(0, 0, -0), Quaternion.identity);
            obj.GetComponent<OnHitEffect>().HitEnemy = true;
            obj.GetComponent<OnHitEffect>().PauseTime = PauseOnHit;
        }


        var laser = (GameObject)Instantiate(Resources.Load(LaserPrefabString));
        laser.GetComponent<LaserFade>().width = LaserWidth;

        var line = laser.GetComponent<LineRenderer>();

        line.SetPosition(0, new Vector3(wielder.transform.position.x, wielder.transform.position.y - 0.5f));
        line.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -0.5f));

        base.AltFire();
    }
        
}
