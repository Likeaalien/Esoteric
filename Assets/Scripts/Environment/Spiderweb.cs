using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spiderweb : MonoBehaviour, IHittable
{
    private int spiderweb_hp = 3;

    public void OnHit(MeleeWeapon weapon)
    {
        if (weapon.weapon_type != MeleeType.Tool_Axe && weapon.weapon_type != MeleeType.Sharp)
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
