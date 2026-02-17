using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class Female_1 : MonoBehaviour, IInteractable
{

    private TextMeshPro npc_name_female;

    public NPCDialog hope_dialogue_data;
    public bool CanInteract()
    {
        return true;
    }
    public void Interact(Player player)
    {
        player.start_dialogue(hope_dialogue_data);
    }

    void Awake()
    {
        TextMeshPro npc_name_female = GetComponentInChildren<TextMeshPro>();
        npc_name_female.text = "Hope";
    }
}
