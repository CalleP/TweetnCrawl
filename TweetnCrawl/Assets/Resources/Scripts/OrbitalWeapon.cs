using UnityEngine;
using System.Collections;

public class OrbitalWeapon : BaseWeapon {

    public float damageRadius = 1.5f;
    public int damage = 3;
	void Start () {
        
        coolDown = 1f;
	}
	
	// Update is called once per frame
	void Update () {


	}

    public override void Fire()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (canFire())
        {
            Instantiate(Resources.Load("BlueExplosion"), p, Quaternion.identity);
            base.Fire();
            var hitTargets = Physics2D.OverlapCircleAll(p, damageRadius);
            foreach (var target in hitTargets)
            {
                if (target.gameObject.tag == "Enemy")
                {
                    target.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage);
                }
            }
        }
    }
}
