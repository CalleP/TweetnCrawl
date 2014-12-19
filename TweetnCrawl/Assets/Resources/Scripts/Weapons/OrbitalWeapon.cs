﻿using UnityEngine;
using System.Collections;

public class OrbitalWeapon : BaseWeapon {


    public float damageRadius = 1.5f;
	void Start () {
        
        coolDown = 1f;
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
                    target.gameObject.GetComponent<EnemyScript>().receiveDamage(damage);
                }
            }
        }
    }
}
