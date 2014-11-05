using UnityEngine;
using System.Collections;

public abstract class BaseWeapon : MonoBehaviour {

    protected AudioSource[] audios;

    public Vector3 AimPoint;
    public GameObject wielder = GameObject.Find("Player");
    protected int damage = 20;
    protected float coolDown = 1f;
    protected float timeStamp = 0f;


    public BaseWeapon()
    {
        audios = wielder.GetComponents<AudioSource>();
    }

    // Use this for initialization
	public virtual void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	     AimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

    public virtual void Fire()
    {
        timeStamp = Time.time + coolDown;
        PlayFireSound();
    }

    protected AudioClip fireSound = Resources.Load<AudioClip>("Sounds/ShotGunFire");
    public virtual void PlayFireSound()
    {
        if (fireSound != null)
        {
            audios[0].clip = fireSound;
            audios[0].Play();
            PlayCooldownSound();

        }

    }
    protected AudioClip reloadSound = Resources.Load<AudioClip>("Sounds/ShotGunReload");
    public virtual void PlayCooldownSound()
    {
        if (reloadSound != null)
        {
            audios[1].clip = reloadSound;
            audios[1].PlayDelayed(fireSound.length - 0.8f);


        }

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
