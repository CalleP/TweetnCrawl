using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    private BaseWeapon currentWeapon;
    public List<BaseWeapon> weapons;
    public int WeaponPackSize = 1;
	// Use this for initialization
	void Start () {
        weapons = new List<BaseWeapon>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0) && weapons.Count != 0)
        {
            currentWeapon.Fire();
        }

        if (Input.GetKey(KeyCode.Mouse1) && weapons.Count != 0)
        {
            currentWeapon.AltFire();
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

    public void PickUp(PickupBase pickUp) 
    {
        var item = pickUp.Item;
        if (item.GetType().IsSubclassOf(typeof(BaseWeapon)))
        {

                replaceWeapon((BaseWeapon)item);
            
        }
        //Add code for other pickups

    }

    private void replaceWeapon(BaseWeapon weapon)
    { 
        if (weapons.Count >= WeaponPackSize)
        {
            dropWeapon(currentWeapon);
            currentWeapon = weapon;
        }
        else
        {
            weapons.Add(weapon);
            EquipWeapon(weapons.Count - 1);
        }
    }

    private void dropWeapon(BaseWeapon weapon)
    {
        var pickUp = (GameObject)Instantiate(Resources.Load("Pickup"),new Vector3(transform.position.x,transform.position.y, 0),Quaternion.identity);
        var pickUpScript = pickUp.GetComponent<PickupBase>();
        pickUpScript.Item = weapon;
    }


}
