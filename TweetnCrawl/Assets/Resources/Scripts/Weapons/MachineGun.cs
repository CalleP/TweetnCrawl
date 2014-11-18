using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class MachineGun : Revolver
{
    private System.Random rand = new System.Random();
    public MachineGun() : base()
    {
        fireSound = null;
        coolDown = 0.12f;
        altCoolDown = 0.6f;
        Spread = 8;
        altDamage = 100;
        SemiAuto = false;
    }

    public override void AltFire()
    {
        if (canFire())
        {
            var objectPos = wielder.transform.position;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);



            mousePos = new Vector3(mousePos.x + rand.Next(AltSpread * -1, AltSpread), mousePos.y + rand.Next(AltSpread * -1, AltSpread), mousePos.z);
            var hits = Physics2D.RaycastAll(wielder.transform.position, (mousePos-objectPos).normalized, 80f);

            var closestHit = 5000f;
            var farthestHit = 0f;
            GameObject bestMatch = null;
            RaycastHit2D hit = new RaycastHit2D();
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance <= closestHit)
                {
                    if (hits[i].collider.gameObject.tag == "Wall")
                    {
                        closestHit = hits[i].distance;
                        bestMatch = hits[i].collider.gameObject;
                        hit = hits[i];
                        break;
                    }

                }
                if (hits[i].distance >= farthestHit && hits[i].collider.gameObject.tag != "Wall")
                {
                    closestHit = hits[i].distance;
                    bestMatch = hits[i].collider.gameObject;
                    hit = hits[i];
                }

                if (hits[i].collider.gameObject.tag == "Enemy")
                {
                    hits[i].collider.gameObject.GetComponent<EnemyScript>().receiveDamage(altDamage);
                }
            }


            //var objectPos2 = wielder.transform.position;
            //var mousePos2 = bestMatch.transform.position;
            //mousePos.x = mousePos.x - objectPos.x;
            //mousePos.y = mousePos.y - objectPos.y;
            //var angle2 = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            //Quaternion outRotation2 = Quaternion.Euler(new Vector3(0, 0, angle2));

            var laser = (GameObject)Instantiate(Resources.Load("Lazor"));

            var line = laser.GetComponent<LineRenderer>();

            line.SetPosition(0, wielder.transform.position);
            line.SetPosition(1, hit.point);


            //laser.transform.position += (laser.transform.up*10);

            base.AltFire();
        }

    }
        
}
