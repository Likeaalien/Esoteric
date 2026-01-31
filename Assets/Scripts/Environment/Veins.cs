using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VeinType
{
    GoldVein,
    OreVein,
    EmptyVein
}
public class Veins : MonoBehaviour, IHittable
{
    [SerializeField] private VeinType vein_type;
    private int vein_hp = 12;

    public void OnHit(MeleeWeapon weapon)
    {
        if (weapon.weapon_type != MeleeType.Tool_Pickaxe)
            return;

        if (vein_hp > 0)
        {
            vein_hp -= 1;
        }
        if (vein_hp % 3 == 0 && vein_hp < 12)
        {
            SpawnNugget();
        }
        if (vein_hp == 0)
        {
            Destroy(gameObject);
        }
    }
    public void SpawnNugget()
    {
        switch (vein_type)
        {
            case VeinType.GoldVein:
                GameObject gold = Resources.Load<GameObject>("Prefabs/GoldNugget");
                Instantiate(gold, transform.position + Vector3.down, Quaternion.identity);
                break;
            case VeinType.OreVein:
                GameObject diamond = Resources.Load<GameObject>("Prefabs/OreNugget");
                Instantiate(diamond, transform.position + Vector3.down, Quaternion.identity);
                break;
        }
    }
}
