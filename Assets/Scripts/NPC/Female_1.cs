using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class Female_1 : MonoBehaviour, IInteractable
{
    private TextMeshPro npc_name;
    private GameObject npc_interact_icon;

    public NPCDialog hope_dialogue_data;

    void Awake()
    {
        npc_name = GetComponentInChildren<TextMeshPro>();
        npc_name.text = "Hope";
    }
    public bool CanInteract()
    {
        return true;
    }
    public void CreateInteractionIcon()
    {
        if(CanInteract())
            npc_interact_icon = Instantiate(Resources.Load<GameObject>("UI/InteractionIcon"), npc_name.transform.position + new Vector3(-0.7f, 0f, 0f), Quaternion.identity);
    }

    public void Interact(Player player)
    {
        player.start_dialogue(hope_dialogue_data);
    }

    public void DestroyInteractionIcon()
    {
        if(npc_interact_icon != null)
            Destroy(npc_interact_icon);
    }


}
