using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Weapon_test,
    Rock_Dwayne,
    Arrow_test
}

public class Pickup : MonoBehaviour
{
    public PickupType pickup_type;
}