using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;


class TeleporterEnemy : BasicEnemy
{
    public GameObject TeleporterEffect;
    public float TeleportCooldown;
    private float time3;
    public override void Update()
    {

        base.Update();

        distance = Vector3.Distance(Follower.position, player.position);
        if (distance < chaseRange && time3 <= Time.time)
        {
            var obj = (GameObject)Instantiate(TeleporterEffect, new Vector3(transform.position.x,transform.position.y, -0.5f), Quaternion.identity);

            time2 = Time.time + (float)AttackDelay;
            Teleport();

            obj = (GameObject)Instantiate(TeleporterEffect, new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);

            time3 = Time.time + TeleportCooldown;
        }
        

    }

    public void Teleport()
    {
        var tile = ObjectPlacer.findAvailableCloseToPlayer(5);
        transform.position = new Vector3(tile.X * 3.2f, tile.Y * 3.2f, transform.position.z);
    }
}

