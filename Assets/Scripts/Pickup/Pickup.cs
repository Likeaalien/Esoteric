using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Weapon_Sword,
    Weapon_Axe,
    Weapon_Pickaxe,
    Weapon_Rock,
    Weapon_Bow,
    Resource_Wood,
    Resource_Gold,
    Resource_Ore,
    Wooden_key
}

public class Pickup : MonoBehaviour
{
    public PickupType pickup_type;
}