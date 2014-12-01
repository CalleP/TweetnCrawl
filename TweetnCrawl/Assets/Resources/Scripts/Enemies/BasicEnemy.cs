
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
        //InvokeRepeating("base.patrol", 1f, 2f); 


       // speed = 0.3f;

    }

    public virtual void Update()
    {
        Flip();
		patrol ();
        //if the enemy followers health reaches 0 remove him from the game.
        if (health <= 0)
        {
            print("Blaaah you killed me!");
            Destroy((Follower as Transform).gameObject);
        }

        //Updates constantly the distance between the follower and the player


        
       // randomPosition = new Vector3(transform.position.x + Random.Range(10f, -10f), transform.position.y + Random.Range(8f, -8f), 0f);




        //if the distance gets within the chaseRange the follower will start following the player


        distance = Vector3.Distance(Follower.position, player.position);
        if (time2 <= Time.time && distance <= 75)
        {
            if (isPlayerInLineOfSight())
			{	patrol();
                ShootAtPlayer();
                time2 = Time.time + (float)AttackDelay;
            }

        }
    }

   





    

    //public override void patrol()
   // {
   //     StartCoroutine(patrolUpdate());
   // }

    protected bool isPlayerInLineOfSight()
    {
        distance = Vector3.Distance(Follower.position, player.position);
        bool hitPlayer = false;
        bool LOS = true;



        var hits = Physics2D.LinecastAll((Vector2)transform.position, (Vector2)player.transform.position);
        for (int i = 0; i < hits.Length; i++)
        {
            var hitDistance = (hits[i].point - (Vector2)transform.position).magnitude;
            if (hits[i].transform.gameObject.tag == "Player")
            {
                hitPlayer = true;
            }
            else if (hits[i].transform.gameObject.tag == "Wall" && hitDistance <= distance)
            {
                LOS = false;
            }








        }
        if (hitPlayer && LOS)
        {
            return true;

        }
        return false;

    }

   // protected override IEnumerator patrolUpdate()
   // {

     //   while (true)
      //  {
        //    float randomWait = Random.Range(2f, 3f);
         //   point = new Vector3(Random.Range(0.1f, -0.1f), Random.Range(0.1f, -0.1f), 0f);
          //  point = point.normalized;
           // InvokeRepeating("MoveToPoint", 0f, 0.1f);
           // yield return new WaitForSeconds(randomWait);
           // CancelInvoke("MoveToPoint");
           // randomWait = Random.Range(0.2f, 0.5f);
            //yield return new WaitForSeconds(randomWait);
        //}

    //}

    //private Vector3 point;

   // public void MoveToPoint()
    //{
     //   rigidbody2D.MovePosition(transform.position + point);// = Vector3.MoveTowards(transform.position, point, speed);
    //}


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

        //print("shooting at player");
    }


}

