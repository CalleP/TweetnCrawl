using UnityEngine;
using System.Collections;

public abstract class BaseWeapon : ScriptableObject {

    protected AudioSource[] audios;


    public GameObject shell = (GameObject)Resources.Load("Shell");
    public GameObject camera = GameObject.Find("Camera");
    public bool SemiAuto = false;
    public Vector3 AimPoint;
    public GameObject wielder = GameObject.Find("Player");
    protected int damage = 20;
    protected int altDamage = 20;
    protected float coolDown = 1f;
    protected float altCoolDown = 1f;
    protected float timeStamp = 0f;
    public WeaponTypes type;
    protected bool altFireEnabled = true;
    protected float ShakeMagnitude = 0.1f;
    protected float ShakeDuration = 0.1f;
    public float PauseOnHit = 0.02f;
    public int AmmoCost = 1;

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
        camera.GetComponent<SmoothCamera2D>().shake(ShakeMagnitude, ShakeDuration);
        PlayFireSound();
    }

    public virtual void AltFire()
    {
        if (altFireEnabled)
        {
            timeStamp = Time.time + altCoolDown;
            camera.GetComponent<SmoothCamera2D>().shake(ShakeMagnitude, ShakeDuration);
            PlayAltFireSound();
        }

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

    protected AudioClip altFireSound = Resources.Load<AudioClip>("Sounds/ShotGunFire");
    public virtual void PlayAltFireSound()
    {
        if (altFireSound != null)
        {
            audios[0].clip = altFireSound;

            audios[0].Play();
            PlayCooldownSound();

        }

    }


    public virtual bool canFire()
    {
        if (timeStamp <= Time.time)
        {

            return true;
            
        }
        
        return false;

    }




}
