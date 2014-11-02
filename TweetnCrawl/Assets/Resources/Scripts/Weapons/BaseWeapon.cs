using UnityEngine;
using System.Collections;

public abstract class BaseWeapon : MonoBehaviour {

    public Vector3 AimPoint;
    public GameObject wielder = GameObject.Find("Player");
    protected int damage;
    protected float coolDown = 1f;
    protected float timeStamp = 0f;


    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	     AimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
