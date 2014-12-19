using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScatterProjectile : BaseProjectile {

    public int amountOfSubProjectiles = 8;
    public GameObject projectile;
    public List<BaseProjectile> projectiles = new List<BaseProjectile>();
    public int spread = 3;

    System.Random rand = new System.Random();

	void Start () {
        speed = 40;
	}
	
	

    public override void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Enemy")
        {
            Scatter();
            Destroy(gameObject);
        }

        
    }

    public override void FixedUpdate()
    {
        speed -= (0.2f)*speed/10;
        speed -= 0.3f;
        base.FixedUpdate();
    }

    void Update()
    {
        if (speed <= 5)
        {
            Scatter();
            Destroy(gameObject);
        }
    }

    public void Scatter()
    {

            for (int i = 0; i < amountOfSubProjectiles; i++)
            {
                var proj = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);

                var projectileScript = proj.GetComponent<BaseProjectile>();

                projectileScript.Init(transform.position, proj.transform.rotation, 35, 30, weapon);
                proj.transform.Rotate(new Vector3(0, 0, i * 30f + rand.Next(spread*-1, spread)));


            }

       
    }
}
