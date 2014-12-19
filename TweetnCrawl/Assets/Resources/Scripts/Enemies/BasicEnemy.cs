
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using System.Collections;



class BasicEnemy : BaseEnemy
{

    public float ProjectileSpeed;
    public int ProjectileDamage;
    public float ProjectileSpread;


    protected float time2;
    void Start()
    {

        baseReferences();

    }

    public virtual void Update()
    {
        Flip();
		patrol ();
        //if the enemy followers health reaches 0 remove him from the game.
        if (health <= 0)
        {
            print("Blaaah you killed me!");
			//StartCoroutine(ShowMessage());
            StartCoroutine(OnDeathEffect());

        }



        distance = Vector3.Distance(Follower.position, player.position);
        if (time2 <= Time.time && distance <= 54)
        {
            if (isPlayerInLineOfSight())
			{	patrol();
                ShootAtPlayer();
                if (Follower.position.x - player.position.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                time2 = Time.time + (float)AttackDelay;
            }

        }
    }


    public void ShootAtPlayer()
    {
        var bullet = (GameObject)Instantiate(Projectile, transform.position, Quaternion.identity);
        var script = bullet.GetComponent<BaseProjectile>();

        var objectPos = transform.position;
        var playerPos = player.position;
        playerPos.x = playerPos.x - objectPos.x;
        playerPos.y = playerPos.y - objectPos.y;
        var angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;

        Quaternion outRotation = Quaternion.Euler(new Vector3(0, 0, angle - Random.Range(-ProjectileSpread, ProjectileSpread)));

        script.Init((player.position - transform.position).normalized, outRotation, ProjectileSpeed, ProjectileDamage);

    }
}

