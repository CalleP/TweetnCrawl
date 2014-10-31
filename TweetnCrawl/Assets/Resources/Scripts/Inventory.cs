using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    private BaseWeapon currentWeapon;
    public List<BaseWeapon> weapons;
    public int WeaponPackSize = 3;
	// Use this for initialization
	void Start () {
        weapons = new List<BaseWeapon>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            currentWeapon.Fire();
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


}
