using TMPro;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private NPCDialog npc_dialogue_data;
    protected TextMeshPro npc_name;
    private GameObject npc_interact_icon;
    void Awake()
    {
        npc_name = GetComponentInChildren<TextMeshPro>();
    }

    public bool CanInteract()
    {
        return true;
    }
    public void CreateInteractionIcon()
    {
        if(CanInteract())
            npc_interact_icon = Instantiate(Resources.Load<GameObject>("UI/InteractionIcon"), npc_name.transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
    public void Interact(Player player)
    {
        player.start_dialogue(npc_dialogue_data);
    }
    public void DestroyInteractionIcon()
    {
        if(npc_interact_icon != null)
            Destroy(npc_interact_icon);
    }
}