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
    AutoShotgun,
    SawnOff,
    RailGun,
    MiniGun,
    RaveGun,
    Launcher
}
public class PickupWeapon : PickupBase
{
    public static Dictionary<WeaponTypes, Type> WepTypes = new Dictionary<WeaponTypes, Type> 
    { 
        {WeaponTypes.revolver, typeof(DualRevolvers)},
        {WeaponTypes.machineGun, typeof(MachineGun)},
        {WeaponTypes.MiniGun, typeof(Minigun)},
        {WeaponTypes.shotgun, typeof(ShotgunWeapon)},
        {WeaponTypes.AutoShotgun, typeof(AutoShotgun)},
        {WeaponTypes.SawnOff, typeof(SawnOff)},
        {WeaponTypes.RailGun, typeof(RailGun)},
        {WeaponTypes.RaveGun, typeof(RaveGun)},
        {WeaponTypes.Launcher, typeof(Launcher)}
    };

    public WeaponTypes selectedWeapon = WeaponTypes.revolver;
    
    private static int test = 0;
    
    void Start()
    {
            Item = instantiateWeaponType(selectedWeapon);
            textMesh = transform.GetChild(0).GetComponent<TextMesh>();
    }

    TextMesh textMesh;
    public void Update() 
    {
        textMesh.text = Enum.GetName(typeof(WeaponTypes), selectedWeapon);
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

