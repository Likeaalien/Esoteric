using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spiderweb : MonoBehaviour, IHittable
{
    public int spiderweb_hp = 3;

    public void OnHit(Weapon weapon)
    {
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
