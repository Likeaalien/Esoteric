using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Weapon_Sword,
    Weapon_Axe,
    Rock_Dwayne,
    Arrow_test
}

public class Pickup : MonoBehaviour
{
    public PickupType pickup_type;
}