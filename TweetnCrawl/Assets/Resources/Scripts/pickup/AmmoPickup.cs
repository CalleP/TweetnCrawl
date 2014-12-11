using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;


class AmmoPickup : PickupBase
{
    public float FadeTime = 1.5f;
    public int AmmoAmount = 25;
    public bool willDelete = true;
    private bool opened = false;

    void Start()
    {
        if (willDelete)
        {
            StartCoroutine(Fade(5));
        }

    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player" && !opened)
        {
            coll.gameObject.GetComponent<Inventory>().Ammo += AmmoAmount;
            GetComponent<Animator>().PlayOnAwake = true;
            audio.Play();

            opened = true;
        }
    }

    public IEnumerator Fade(float BeforeStart)
    {
        yield return new WaitForSeconds(BeforeStart);
        var portion = FadeTime / 5;
        yield return new WaitForSeconds(portion * 4);
        gameObject.renderer.enabled = false;
        yield return new WaitForSeconds(portion / 2);
        gameObject.renderer.enabled = true;
        yield return new WaitForSeconds(portion / 1.5f);
        gameObject.renderer.enabled = false;
        yield return new WaitForSeconds(portion / 1);
        gameObject.renderer.enabled = true;
        yield return new WaitForSeconds(portion / 0.5f);

        if (willDelete)
        {
            Destroy(gameObject);
        }

        yield return null;
    }



}

