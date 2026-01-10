using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirchTree : MonoBehaviour, IHittable
{
    private int tree_hp = 3;

    public void OnHit(Weapon weapon)
    {
        if (weapon is not MeleeWeapon)
        {
            return;
        }
      
        MeleeWeapon melee_weapon = (MeleeWeapon)weapon;
        if(melee_weapon.weapon_type != MeleeType.Tool)
        {
            return;
        }

        if (tree_hp > 0)
        {
            tree_hp -= 1;
        }
        Debug.Log("Testing" + tree_hp);
        if (tree_hp == 0)
        {
            Destroy(gameObject);
        }
    }
}
