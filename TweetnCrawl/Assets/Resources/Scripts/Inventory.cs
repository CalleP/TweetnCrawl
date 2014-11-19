using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    private BaseWeapon currentWeapon;
    public List<BaseWeapon> weapons;
    public int WeaponPackSize = 3;
	// Use this for initialization
	void Start () {
        weapons = new List<BaseWeapon>();
        weapons.Add(new DualRevolvers());
        EquipWeapon(0);
        
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && weapons.Count != 0)
            {
                currentWeapon.Fire();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && weapons.Count != 0)
            {
                currentWeapon.AltFire();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0) && weapons.Count != 0)
            {
                currentWeapon.Fire();
            }

            if (Input.GetKey(KeyCode.Mouse1) && weapons.Count != 0)
            {
                currentWeapon.AltFire();
            }
        }

	}

    public void EquipWeapon(int index) 
    {
        if (weapons.Count >= 0 && index <= weapons.Count-1)
        {
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


}
