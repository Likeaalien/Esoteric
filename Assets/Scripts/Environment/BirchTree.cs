using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirchTree : MonoBehaviour, IHittable
{
    private int tree_hp = 3;

    public void OnHit(Weapon weapon)
    {
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
