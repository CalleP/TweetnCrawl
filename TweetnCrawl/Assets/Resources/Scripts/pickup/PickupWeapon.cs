using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;





public enum WeaponTypes
{
    shotgun,
    machineGun,
    revolver,
    laserMiniGun
}
public class PickupWeapon : PickupBase
{
    public static Dictionary<WeaponTypes, Type> WepTypes = new Dictionary<WeaponTypes, Type> 
    { 
        {WeaponTypes.revolver, typeof(DualRevolvers)},
        {WeaponTypes.machineGun, typeof(MachineGun)},
        {WeaponTypes.laserMiniGun, typeof(LaserMinigun)},
        {WeaponTypes.shotgun, typeof(ShotgunWeapon)}
    };

    public WeaponTypes selectedWeapon = WeaponTypes.revolver;
    
    private static int test = 0;
    
    void Start()
    {
            Item = instantiateWeaponType(selectedWeapon);
    }

    public static BaseWeapon instantiateWeaponType(WeaponTypes type)
    {
        return  (BaseWeapon)Activator.CreateInstance(WepTypes[type]);
    }

    public static WeaponTypes TypeToWeaponType(Type type)
    {
        return WepTypes.FirstOrDefault(x => x.Value == type).Key;
    }

    private float timeStamp;
    protected override void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.E) && timeStamp <= Time.time)
        {
            Debug.Log("KeyDown");
            coll.gameObject.GetComponent<Inventory>().PickUpWeapon(instantiateWeaponType(selectedWeapon), this);
            timeStamp = Time.time + 0.5f;
            
        }
    }
}

