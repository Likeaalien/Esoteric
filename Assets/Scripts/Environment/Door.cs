using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject invisible_wall;
    private GameObject interact_icon;
    public NPCDialog door_dialogue_data;   
    public bool CanInteract()
    {
        return true;
    }
    public void CreateInteractionIcon()
    {
        if(CanInteract())
            interact_icon = Instantiate(Resources.Load<GameObject>("UI/InteractionIcon"), transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
    }
    public void Interact(Player player)
    {
        if (player.wooden_key == false)
        {
            door_dialogue_data.npc_name = player.Players_Name;
            player.start_dialogue(door_dialogue_data);
            return;
        }
        Destroy(gameObject);
        Destroy(invisible_wall);
        Instantiate(Resources.Load<GameObject>("Prefabs/Wooden_door_open"), transform.position, Quaternion.identity);
    }
    public void DestroyInteractionIcon()
    {
        if(interact_icon != null)
            Destroy(interact_icon);
    }   
}
