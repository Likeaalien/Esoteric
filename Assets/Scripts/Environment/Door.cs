using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject invisible_wall;
    public NPCDialog door_dialogue_data;

    public bool CanInteract()
    {
        return true;
    }
    public void Interact(Player player)
    {
        if (player.wooden_key == false)
        {
            player.start_dialogue(door_dialogue_data);
            return;
        }

        Destroy(gameObject);
        Destroy(invisible_wall);
        Instantiate(Resources.Load<GameObject>("Prefabs/Wooden_door_open"), transform.position, Quaternion.identity);
    }
}
