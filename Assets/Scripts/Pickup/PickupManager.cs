using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public void HandlePickup(Player player, Pickup pickup)
    {
        switch(pickup.pickup_type)
        {
            case PickupType.Weapon_Sword:
                PickupWeaponSword(player);
                break;
            case PickupType.Weapon_Axe:
                PickupWeaponAxe(player);
                break;
            case PickupType.Weapon_Pickaxe:
                PickupWeaponPickaxe(player);
                break;
            case PickupType.Weapon_Bow:
                PickupBow(player);
                break;
            case PickupType.Wood:
                PickupResourcesWood(player);
                break;
            case PickupType.Rock_Dwayne:
                PickupRock(player);
                break;  
        }
        Destroy(pickup.gameObject);
    }
    void PickupWeaponSword(Player player)
    {
        player.player_current_weapon = new MeleeWeapon(50, 0.1f, 1f, MeleeType.Sharp);
        player.equipped_weapon_prefab = Resources.Load<GameObject>("Prefabs/Sword");
        player.ChangeSprite("Sword");
    }
    void PickupWeaponAxe(Player player)
    {
        player.player_current_weapon = new MeleeWeapon(5, 0.1f, 1f, MeleeType.Tool_Axe);
        player.equipped_weapon_prefab = Resources.Load<GameObject>("Prefabs/Axe"); 
        player.ChangeSprite("Axe");   
    }
    void PickupWeaponPickaxe(Player player)
    {
        player.player_current_weapon = new MeleeWeapon(10, 0.1f, 1f, MeleeType.Tool_Pickaxe);
        player.equipped_weapon_prefab = Resources.Load<GameObject>("Prefabs/Pickaxe"); 
        player.ChangeSprite("Pickaxe");   
    }
    void PickupBow(Player player)
    {
        player.player_current_weapon = new RangeWeapon(50, 0f, player.arrow_prefab, 10, 500);
        player.equipped_weapon_prefab = Resources.Load<GameObject>("Prefabs/Bow"); 
        player.ChangeSprite("Bow");
    }
    void PickupRock(Player player)
    {
        player.player_current_weapon = new RangeWeapon(10, 0f, player.rock_prefab, 10, 150);
        player.ChangeSprite("Unarmed");
    }
    void PickupResourcesWood(Player player)
    {
        player.wood_currency += 1;
    }
}
