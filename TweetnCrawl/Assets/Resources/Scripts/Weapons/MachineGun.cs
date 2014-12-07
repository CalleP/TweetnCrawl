using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class MachineGun : Revolver
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
        BulletSpeed = 60f;
        Spread = 8;
        altDamage = 50;
        SemiAuto = false;
        type = WeaponTypes.machineGun;
        
    }

    public override void AltFire()
    {
        if (canFire() && altFireEnabled)
        {
            var objectPos = wielder.transform.position;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);



            mousePos = new Vector3(mousePos.x + rand.Next(AltSpread * -1, AltSpread), mousePos.y + rand.Next(AltSpread * -1, AltSpread), wielder.transform.position.z);
            var hits = Physics2D.RaycastAll(wielder.transform.position, (mousePos-objectPos).normalized, 200f);

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

            for (int i = 0; i < hits.Length; i++)
            {
                float distance = (hits[i].point - (Vector2)wielder.transform.position).magnitude;
                if (hits[i].collider.gameObject.tag == "Enemy" && distance <= closestHit)
                {
                    hits[i].collider.gameObject.GetComponent<BaseEnemy>().receiveDamage(altDamage);
                }
            }


            //var objectPos2 = wielder.transform.position;
            //var mousePos2 = bestMatch.transform.position;
            //mousePos.x = mousePos.x - objectPos.x;
            //mousePos.y = mousePos.y - objectPos.y;
            //var angle2 = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            //Quaternion outRotation2 = Quaternion.Euler(new Vector3(0, 0, angle2));

            var laser = (GameObject)Instantiate(Resources.Load(LaserPrefabString));
            laser.GetComponent<LaserFade>().width = LaserWidth;

            var line = laser.GetComponent<LineRenderer>();

            line.SetPosition(0, new Vector3(wielder.transform.position.x, wielder.transform.position.y -0.5f));
            line.SetPosition(1, new Vector3(hit.point.x, hit.point.y , -0.5f));


            //laser.transform.position += (laser.transform.up*10);

            base.AltFire();
        }

    }
        
}
