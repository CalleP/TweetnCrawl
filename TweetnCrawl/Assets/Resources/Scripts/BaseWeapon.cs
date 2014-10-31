using UnityEngine;
using System.Collections;

public abstract class BaseWeapon : MonoBehaviour {

    public GameObject wielder;
    protected int damage;
    protected float coolDown = 1f;
    protected float timeStamp = 0f;


    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Fire()
    {
        timeStamp = Time.time + coolDown;
    }

    public bool canFire()
    {
        if (timeStamp <= Time.time)
        {
            return true;
        }
        return false;

    }
        




}
