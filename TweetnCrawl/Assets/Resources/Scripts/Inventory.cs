using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public Texture2D Cursor;
    private BaseWeapon currentWeapon;
    public List<BaseWeapon> weapons;
    public int WeaponPackSize = 3;
    public Transform ShellEjectionPoint;
    [SerializeField]
    private int ammo = 100;
    public int Ammo
    {
        get { return ammo; }  // Getter
        set { ammo = value; StartCoroutine(PickupFlash()); } // Setter
    }

	// Use this for initialization
	void Start () {
        weapons = new List<BaseWeapon>();
        player = GameObject.Find("Player");
        weapons.Add(new DualRevolvers());
        EquipWeapon(0);
        //UnityEngine.Cursor.SetCursor(Cursor, new Vector2(32,32), CursorMode.Auto);
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(2);
        }

        if (currentWeapon.SemiAuto == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && weapons.Count != 0 && ammo >= currentWeapon.AmmoCost)
            {
                if (currentWeapon.canFire())
                {
                    spawnShell();
                    ammo -= currentWeapon.AmmoCost;
                }
                currentWeapon.Fire();


            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && weapons.Count != 0 && ammo >= currentWeapon.AmmoCost)
            {
                if (currentWeapon.canFire())
                {
                    spawnShell();
                    ammo -= currentWeapon.AmmoCost;
                }

                currentWeapon.AltFire();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0) && weapons.Count != 0 && ammo >= currentWeapon.AmmoCost)
            {
                if (currentWeapon.canFire())
                {
                    spawnShell();
                    ammo -= currentWeapon.AmmoCost;
                }

                currentWeapon.Fire();


            }

            if (Input.GetKey(KeyCode.Mouse1) && weapons.Count != 0 && ammo >= currentWeapon.AmmoCost)
            {
                if (currentWeapon.canFire())
                {
                    spawnShell();
                    ammo -= currentWeapon.AmmoCost;
                }

                currentWeapon.AltFire();
            }
        }

	}

    private GameObject player;
    public void EquipWeapon(int index) 
    {
        if (weapons.Count >= 0 && index <= weapons.Count-1)
        {
            var obj = (GameObject)Instantiate(Resources.Load("EquipEffect"));
            obj.transform.parent = transform;
            obj.transform.position = transform.position+new Vector3(-1.3f,1.5f, 0);
            GameObject.Find("WeaponSwap").GetComponent<AudioSource>().Play();
            currentWeapon = weapons[index];
        }

    }

    public void FireWeapon()
    {
        currentWeapon.Fire();
    }

    public void PickUpWeapon(BaseWeapon weapon, PickupWeapon pickup) 
    {
                replaceWeapon(weapon, pickup);
    }

        //Add code for other pickups

    

    private void replaceWeapon(BaseWeapon weapon, PickupWeapon pickup)
    { 
        if (weapons.Count >= WeaponPackSize)
        {
            
            int index;
            for (index = 0; index < weapons.Count; index++)
            {
                if (weapons[index].GetType() == currentWeapon.GetType()) { break; }
            }
            Debug.Log(index);
            weapons[index] = weapon;
            dropWeapon(currentWeapon, pickup);
            EquipWeapon(index);
        }
        else
        {
            weapons.Add(weapon);
            EquipWeapon(weapons.Count - 1);
            Destroy(pickup.gameObject);
        }
    }

    private void dropWeapon(BaseWeapon weapon, PickupWeapon pickup)
    {
       
        var pickupScript = pickup.GetComponent<PickupWeapon>();
        pickupScript.selectedWeapon = PickupWeapon.TypeToWeaponType(weapon.GetType());
    }

    public Material PickUpFlashMaterial;
    public Color PickUpFlashColor;
    public float PickUpFlashDuration = 2f;
    public IEnumerator PickupFlash()
    {
        var oldMaterial = renderer.material;
        var oldColor = renderer.material.color;
        renderer.material = PickUpFlashMaterial;
        renderer.material.color = PickUpFlashColor;

        yield return new WaitForSeconds(PickUpFlashDuration);

        renderer.material = oldMaterial;
        renderer.material.color = oldColor;

        yield return null;
    }

    public void spawnShell()
    {
        if (currentWeapon.shell != null)
        {
            var shell = (GameObject)Instantiate(currentWeapon.shell, new Vector3(transform.position.x, transform.position.y, -0.25f), Quaternion.identity);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ShellEjectionPoint.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
            ShellEjectionPoint.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(70f,120f)));
            shell.rigidbody2D.AddForce(ShellEjectionPoint.up * UnityEngine.Random.Range(2000f,2500f));
            shell.transform.rotation = ShellEjectionPoint.rotation;
            shell.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-70f, -120)));
            Physics2D.IgnoreCollision(shell.collider2D, player.collider2D);
        }
    }


    void OnGUI()
    {
        GUI.Label(new Rect(5, 310, 200, 200), Ammo.ToString());

    }


}
