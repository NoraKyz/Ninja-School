using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Fly Weapon")]
public class FlyWeaponData : ScriptableObject
{
    public float speed;
    public float damage;
    public float critRate;
    public float thrustForce;
}
