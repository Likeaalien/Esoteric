using TMPro;
using UnityEngine;
using System.Collections;

public class Uancha : MonoBehaviour, IInteractable
{
    private TextMeshPro npc_name;
    private GameObject npc_interact_icon;
    public NPCDialog uancha_dialogue_data; 
    public bool Uancha_Quest_1_is_completed;

    void Awake()
    {
        npc_name = GetComponentInChildren<TextMeshPro>();
        npc_name.text = "Uancha";
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
        player.start_dialogue(uancha_dialogue_data);
    }
    public void DestroyInteractionIcon()
    {
        if(npc_interact_icon != null)
            Destroy(npc_interact_icon);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(Uancha_Quest_1_is_completed)
            return;

        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            if (player.wood_currency < 5)
                return;

            if (player.wood_currency >= 5)
            {
                player.wood_currency -= 5;
                Uancha_Quest_1_is_completed = true;
                Instantiate(Resources.Load<GameObject>("Prefabs/Wooden_key"), transform.position + Vector3.left, Quaternion.identity);
                Instantiate(Resources.Load<GameObject>("Trigger/Trigger_Uancha_2")); 
            }
        }
    }
}
