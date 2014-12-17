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
        get { return ammo; }
        set { ammo = value; StartCoroutine(PickupFlash()); }
    }

	void Start () {
        weapons = new List<BaseWeapon>();
        player = GameObject.Find("Player");
        weapons.Add(new DualRevolvers());
        EquipWeapon(0);

        
	}
	
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

    public void PickUpWeapon(BaseWeapon weapon, PickupWeapon pickup) 
    {
                replaceWeapon(weapon, pickup);
    }

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
        pickupScript.SelectedWeapon = PickupWeapon.TypeToWeaponType(weapon.GetType());
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
        if (currentWeapon.Shell != null)
        {
            var shell = (GameObject)Instantiate(currentWeapon.Shell, new Vector3(transform.position.x, transform.position.y, -0.25f), Quaternion.identity);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ShellEjectionPoint.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
            ShellEjectionPoint.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(70f,120f)));
            shell.rigidbody2D.AddForce(ShellEjectionPoint.up * UnityEngine.Random.Range(2000f,2500f));
            shell.transform.rotation = ShellEjectionPoint.rotation;
            shell.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-70f, -120)));
            Physics2D.IgnoreCollision(shell.collider2D, player.collider2D);
        }
    }

    public GUIStyle AmmoCounterStyle;
    void OnGUI()
    {
        GUI.Label(new Rect(0, Screen.height-115, 200, 200), Ammo.ToString(), AmmoCounterStyle);

    }

    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }

}
