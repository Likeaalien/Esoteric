using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public void HandlePickup(Player player, Pickup pickup)
    {
        switch(pickup.pickup_type)
        {
            case PickupType.Weapon_test:
                PickupWeapon(player);
                break;
            case PickupType.Arrow_test:
                PickupArrow(player);
                break;
            case PickupType.Rock_Dwayne:
                PickupRock(player);
                break;
        }
        Destroy(pickup.gameObject);
    }
    void PickupWeapon(Player player)
    {
        player.player_current_weapon = new MeleeWeapon(50, 0.1f, 1f, MeleeType.Sharp);
        player.equipped_weapon_prefab = Resources.Load<GameObject>("Prefabs/Sword");
        player.ChangeSprite("Sword");
    }
    void PickupArrow(Player player)
    {
        player.player_current_weapon = new RangeWeapon(50, 0f, player.arrow_prefab, 10, 500);
        player.ChangeSprite("Bow");
    }
    void PickupRock(Player player)
    {
        player.player_current_weapon = new RangeWeapon(10, 0f, player.rock_prefab, 10, 150);
        player.ChangeSprite("Unarmed");
    }
}
