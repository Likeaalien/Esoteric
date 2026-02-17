using TMPro;
using UnityEngine;
using System.Collections;

public class Uancha : MonoBehaviour, IInteractable
{
    public bool Uancha_Quest_1_is_completed;
    public NPCDialog uancha_dialogue_data;
    private TextMeshPro npc_name_uancha;    
    
    public bool CanInteract()
    {
        return true;
    }

    public void Interact(Player player)
    {
        player.start_dialogue(uancha_dialogue_data);
    }
    void Awake()
    {
        npc_name_uancha = GetComponentInChildren<TextMeshPro>();
        npc_name_uancha.text = "Uancha";
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
