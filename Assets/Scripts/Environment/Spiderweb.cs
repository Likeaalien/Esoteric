using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spiderweb : MonoBehaviour, IHittable
{
    private int spiderweb_hp = 3;

    public void OnHit(Weapon weapon)
    {
        if (weapon is not MeleeWeapon)
        {
            return;
        }

        MeleeWeapon melee_weapon = (MeleeWeapon)weapon;
        if(melee_weapon.weapon_type != MeleeType.Sharp)
        {
            return;
        }

        if (spiderweb_hp > 0)
        {
            spiderweb_hp -= 1;
        }

        if (spiderweb_hp == 0)
        {
            Destroy(gameObject);
        }
    }
}
