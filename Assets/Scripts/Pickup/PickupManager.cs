using System.Collections;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public void HandlePickup(Player player, Pickup pickup)
    {
        switch (pickup.pickup_type)
        {
            case PickupType.Weapon_Sword:
            case PickupType.Weapon_Axe:
            case PickupType.Weapon_Pickaxe:
            case PickupType.Weapon_Bow:
                EquipWeapon(player, pickup);
                break;

            case PickupType.Resource_Wood:    
            case PickupType.Resource_Gold:
            case PickupType.Resource_Ore:
            case PickupType.Wooden_key:
                AddResource(player, pickup);
                break;
        }
        Destroy(pickup.gameObject);
    }
    void EquipWeapon(Player player, Pickup pickup)
    {
        switch(pickup.pickup_type)
        {
            case PickupType.Weapon_Sword:
                player.player_current_weapon = new MeleeWeapon(50, 0.8f, 1f, MeleeType.Sharp);
                player.equipped_weapon_prefab = Resources.Load<GameObject>("Prefabs/Sword");
                player.ChangeSprite("Sword");
                break;
            case PickupType.Weapon_Axe:
                player.player_current_weapon = new MeleeWeapon(5, 0.8f, 1f, MeleeType.Tool_Axe);
                player.equipped_weapon_prefab = Resources.Load<GameObject>("Prefabs/Axe"); 
                player.ChangeSprite("Axe"); 
                break;
            case PickupType.Weapon_Pickaxe:
                player.player_current_weapon = new MeleeWeapon(10, 0.8f, 1f, MeleeType.Tool_Pickaxe);
                player.equipped_weapon_prefab = Resources.Load<GameObject>("Prefabs/Pickaxe"); 
                player.ChangeSprite("Pickaxe");  
                break;
            case PickupType.Weapon_Bow:
                player.player_current_weapon = new RangeWeapon(50, 0.8f, player.arrow_prefab, 10, 500);
                player.equipped_weapon_prefab = Resources.Load<GameObject>("Prefabs/Bow"); 
                player.ChangeSprite("Bow");
                break;
            case PickupType.Weapon_Rock:
                player.player_current_weapon = new RangeWeapon(10, 0.8f, player.rock_prefab, 10, 150);
                player.ChangeSprite("Unarmed");
                break;
        }
    }
    void AddResource(Player player, Pickup pickup)
    {
        switch(pickup.pickup_type)
        {
            case PickupType.Resource_Wood:
                // player.wood_currency += 1;
                player.UpdateCurrency(0, 1);
                break;
            case PickupType.Resource_Gold:
                // player.gold_currency += 1;
                player.UpdateCurrency(2, 1);
                break;
            case PickupType.Resource_Ore:
                // player.ore_currency += 1;
                player.UpdateCurrency(1, 1);
                break;
            case PickupType.Wooden_key:
                player.wooden_key = true;
                break;
        }
    }
}
