using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Weapon_Sword,
    Weapon_Axe,
    Weapon_Pickaxe,
    Rock_Dwayne,
    Weapon_Bow,
    Wood
}

public class Pickup : MonoBehaviour
{
    public PickupType pickup_type;
}