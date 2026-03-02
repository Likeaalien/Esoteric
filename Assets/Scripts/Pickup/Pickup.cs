using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Weapon_Sword,
    Weapon_Axe,
    Weapon_Pickaxe,
    Weapon_Rock,
    Weapon_Bow,
    Resource_Wood,
    Resource_Gold,
    Resource_Ore,
    Wooden_key
}

public class Pickup : MonoBehaviour, IInteractable
{
    public PickupType pickup_type;
    private GameObject interact_icon;

    public bool CanInteract()
    {
        return true;
    }
    public void CreateInteractionIcon()
    {
        interact_icon = Instantiate(Resources.Load<GameObject>("UI/InteractionIcon"), transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
    public void Interact(Player player)
    {
        player.item_interact(this);
    }
    public void DestroyInteractionIcon()
    {
        if(interact_icon != null)
            Destroy(interact_icon);
    }
}