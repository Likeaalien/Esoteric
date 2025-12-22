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
        }
        Destroy(pickup.gameObject);
    }
    void PickupWeapon(Player player)
    {
        player.player_current_weapon = new MeleeWeapon(50, 0.1f, 0);
    }
}
