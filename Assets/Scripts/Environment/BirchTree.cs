using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirchTree : MonoBehaviour, IHittable
{
    private int tree_hp = 3;

    public void OnHit(MeleeWeapon weapon)
    {      
        if(weapon.weapon_type != MeleeType.Tool_Axe)
        {
            return;
        }
        if (tree_hp > 0)
        {
            tree_hp -= 1;
        }
        if (tree_hp == 0)
        {
            Destroy(gameObject);
            SpawnWood();
        }
    }
    private void SpawnWood()
    {
        GameObject wood = Resources.Load<GameObject>("Prefabs/Wood");
        Instantiate(wood, transform.position, Quaternion.identity);
    }
}
